using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Mottel {
public class BossController : MonoBehaviour {
    private int bustedCannons, currentHealth;
    private bool vulerable;
	public int health;
    public float timeVulnerable;
    public GameObject explosion, cannonLeft, cannonRight;
    private Text healthText;

    private void Start() {
        currentHealth = health;
        healthText = GameObject.Find("Boss Health").GetComponent<Text>();
        healthText.text = "Boss: " + (currentHealth / 15f * 100f).ToString("F0") + "%";
    }

    void OnTriggerEnter2D(Collider2D other) {
		if(!vulerable || other.tag == "Boundary") {
			return;
        } else if (other.tag == "Shots") {
            Destroy(other.gameObject);
            hit();
        }
    }

    void hit() {
        currentHealth--;
        Mathf.Clamp(currentHealth, 0, health);
        healthText.text = "Boss: " + (currentHealth / 15f * 100f).ToString("F0") + "%";
        if (currentHealth < 1) {
            Instantiate(explosion, transform.position, transform.rotation);
            GetComponent<NotifyOfDeath>().DeathKnell();
            Destroy(gameObject);
        }
	}
    
	public void cannonHit() {
        bustedCannons++;
        if(bustedCannons > 0 && bustedCannons % 2 == 0) {
            StartCoroutine(HitMe());
        }
	}

    IEnumerator HitMe() {
        vulerable = true;
        yield return new WaitForSeconds(timeVulnerable);
        vulerable = false;
        cannonLeft.GetComponent<CannonController>().reloadHealth();
        cannonRight.GetComponent<CannonController>().reloadHealth();
    }
}
}