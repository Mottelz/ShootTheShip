using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mottel {
public enum objectType { Enemy1, Enemy2, Boss }

public class NotifyOfDeath : MonoBehaviour {
	private GameController gc;
	public int pointValue;
	public objectType type;
	private void Start() {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null) {
            gc = gameControllerObject.GetComponent<GameController>();
        }
        if (gc == null) {
            Debug.Log("Cannot find 'GameController' script.");
        }
	}

	public void DeathKnell() {
		gc.GetComponent<GameController>().IncreaseScore(pointValue, type);
	}
}
}