using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mottel {
    /// <summary>
    /// Script that makes enemies vulnerable to shots.
    /// </summary>
    public class DestoryByShot : MonoBehaviour {
        public GameObject explosion;
        /// <summary>
        /// If you're hit by a shot, then die and tell us about it. See NotifyofDeath for details.
        /// </summary>
        void OnTriggerEnter2D(Collider2D other) {
            if (other.tag == "Boundary"){
                return;
            } else if (other.tag == "Shots") {
                Destroy(other.gameObject);
            }
            Instantiate(explosion, transform.position, transform.rotation);
            GetComponent<NotifyOfDeath>().DeathKnell();
            Destroy(gameObject);
        }
    }
}