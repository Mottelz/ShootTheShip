using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace Mottel {
public class GameController : MonoBehaviour {
	public Text scoreText, highScoreText, menuText;
    public GameObject enemy1, enemy2, boss, powerUp;
	public float waveWait, bossWait, spawnWait1, spawnWait2;
	public int wave2Size;
    private int deadEnemy1, deadEnemy2, score, scoreMultiplier;
    
    private void Start() {
        score = 0;
        scoreMultiplier = 1;
        scoreText.text = "Score: " + score;
        highScoreText.text = "High Score: " + GameState.Instance.highScore;	
        GameState.Instance.mode = GameMode.Menu;
        LoadMenu();
	}

    private void Update() {
        if(Input.GetKey(KeyCode.Escape)) {
    #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
        }
        if(Input.GetKey(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if(GameState.Instance.mode == GameMode.Menu) {
            if(Input.GetKey(KeyCode.Alpha1)) {
                GameState.Instance.mode = GameMode.Normal;
                menuText.text = "";
                StartCoroutine(ActivateEnemies());
            } else if(Input.GetKey(KeyCode.Alpha2)) {
                GameState.Instance.mode = GameMode.BulletHell;
                menuText.text = "";
                StartCoroutine(ActivateEnemies());
            }
        }

    }

    IEnumerator ActivateEnemies() {
        yield return new WaitForSeconds(2);
        StartCoroutine(InitWave1());
        yield return new WaitForSeconds(waveWait);
        StartCoroutine(InitWave2());
        yield return new WaitForSeconds(bossWait);
        InitBoss();
    }
    
    IEnumerator InitWave1() {
		Instantiate(enemy1, new Vector3(0.0f, 5.5f, 0.0f), Quaternion.identity);
        yield return new WaitForSeconds(spawnWait1);
        Instantiate(enemy1, new Vector3(1.0f, 5.5f, 0.0f), Quaternion.identity);
        Instantiate(enemy1, new Vector3(-1.0f, 5.5f, 0.0f), Quaternion.identity);
        yield return new WaitForSeconds(spawnWait1);
        Instantiate(enemy1, new Vector3(2.0f, 5.5f, 0.0f), Quaternion.identity);
        Instantiate(enemy1, new Vector3(-2.0f, 5.5f, 0.0f), Quaternion.identity);
	}

    IEnumerator InitWave2(){
		for(int i = 0; i < wave2Size; i++){
            Instantiate(enemy2, new Vector3(3.0f, 5.0f, 0.0f), Quaternion.identity);
            yield return new WaitForSeconds(spawnWait2);
		}
    }

    void InitBoss() {
        Instantiate(boss, new Vector3(2.2f, 6.0f, 0.0f), Quaternion.AngleAxis(180, new Vector3(1, 0, 0)));
    }

    public void IncreaseScore(int points, objectType type) {
        score += points * scoreMultiplier;
        scoreText.text = "Score: " + score;
        if (score > GameState.Instance.highScore) {
                GameState.Instance.highScore = score;
            highScoreText.text = "High Score: " + GameState.Instance.highScore;
        }

        switch (type) {
            case objectType.Enemy1:
                deadEnemy1++;
                if(deadEnemy1 == 5) {
                    scoreMultiplier++;
                    PowerUp();
                }
                break;
            case objectType.Enemy2:
                deadEnemy2++;
                if(deadEnemy2 == wave2Size) {
                    scoreMultiplier++;
                    PowerUp();
                }
                break;
            case objectType.Boss:
                    LoadMenu();
                break;
            default:
                break;
        }
    }

    private void PowerUp() {
        Instantiate(powerUp, new Vector3(0.0f, 5.5f, 0.0f), transform.rotation);
    }

    void LoadMenu() {
        menuText.text = "SHOOT THE SHIP\n Select Game Mode\n 1. Normal Mode\n 2. Bullet Hell";
    }
}
}