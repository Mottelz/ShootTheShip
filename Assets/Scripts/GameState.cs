using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mottel {
    public enum GameMode {Menu, Normal, BulletHell};
    [System.Serializable]
    public class Boundry {
        public float xMin, xMax, yMin, yMax;
    }

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

