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
        private SpriteRenderer sr;


        /// <summary>
        /// On start, set health. 
        /// </summary>
        private void Start() {
            sr = GetComponent<SpriteRenderer>();
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
                StartCoroutine(smoothTrans(0f, 0.5f));
                ship.GetComponent<BossController>().cannonHit();
            }
        }

        /// <summary>
        /// Sets health back to full, called by the BossController. 
        /// </summary>
        public void reloadHealth() {
            StartCoroutine(smoothTrans(1f, 0.5f));
            health = defaultHealth;
        }

        /// <summary>
        /// A smooth transition for opacity using lerp and fixedupdate.
        /// </summary>
        IEnumerator smoothTrans(float end, float duration) {
            float elapsed = 0f;
            float start = sr.color.a;
            while (sr.color.a != end) {
                float newVal = Mathf.Lerp(start, end, (elapsed/duration));
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, newVal);
                elapsed += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}