using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mottel {
    /// <summary>
    /// Controller for the boss's cannons. 
    /// </summary>
    public class CannonController : MonoBehaviour {
        public GameObject ship, shot, barrel, explosion;
        public int defaultHealth, cooldown;
        private int health;
        private float nextShot;

        /// <summary>
        /// On start, set health. 
        /// </summary>
        private void Start() {
            health = defaultHealth;
        }
        
        /// <summary>
        /// Fire a bullet, if health and time allow it. 
        /// </summary>
        private void Update() {
            if (Time.time > nextShot && health > 0) {
                Instantiate(shot, barrel.transform.position, barrel.transform.rotation);
                nextShot = Time.time + cooldown + (Random.value * cooldown);
            }
        }

        /// <summary>
        /// If hit by a bullet and not dead, take the hit.
        /// </summary>
        void OnTriggerEnter2D(Collider2D other) {
            if (other.tag == "Shots" && health > 0) {
                Destroy(other.gameObject);
                hit();
            }
        }

        /// <summary>
        /// When hit, cause instantiate exposion, reduce health, and if helath is now zero tell the BossController. 
       /// </summary>
        void hit() {
            Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z - 2), transform.rotation);
            health--;
            if(health < 1) {
                ship.GetComponent<BossController>().cannonHit();
            }
        }

       /// <summary>
        // Sets health back to full, called by the BossController. 
       /// </summary>
        public void reloadHealth() {
            health = defaultHealth;
        }
    }
}