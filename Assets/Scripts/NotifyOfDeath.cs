using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mottel {
    
	/// <summary>
    /// Called on a ship's destruction. Used to increase score.
    /// </summary>
    public class NotifyOfDeath : MonoBehaviour {
		private GameController gc;
		public int pointValue;
		public objectType type;

        /// <summary>
        /// Find the GameController.
        /// </summary>
        private void Start() {
			GameObject gameControllerObject = GameObject.FindWithTag("GameController");
			if (gameControllerObject != null) {
				gc = gameControllerObject.GetComponent<GameController>();
			}
			if (gc == null) {
				Debug.Log("Cannot find 'GameController' script.");
			}
		}
        
		/// <summary>
        /// Increase the score in the GameController and tell the GC what kind of ship died.
        /// </summary>
        public void DeathKnell() {
			gc.GetComponent<GameController>().IncreaseScore(pointValue, type);
		}
	}
}