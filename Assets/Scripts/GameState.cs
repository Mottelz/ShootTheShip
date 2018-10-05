using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mottel {
    /// <summary>
    /// Preserves high score and game state. This is a static class.
    /// </summary>
    public class GameState : MonoBehaviour {
        public static GameState Instance { get; private set;}
        public GameMode mode;
        public int highScore;
        private void Awake() {
            if (Instance != null) {
                Destroy(this.gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}

