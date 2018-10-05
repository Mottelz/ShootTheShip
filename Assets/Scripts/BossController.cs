using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mottel {
    // <Summary>
    // The script that controls the Boss. 
    // </Summary>
    public class BossController : MonoBehaviour {
        private int bustedCannons, currentHealth;
        private bool vulerable;
        public int health;
        public float timeVulnerable;
        public GameObject explosion, cannonLeft, cannonRight;
        private Text healthText;
        
        /// <Summary>
        /// Set health, grab text and set it. 
        /// </Summary>
        private void Start() {
            currentHealth = health;
            healthText = GameObject.Find("Boss Health").GetComponent<Text>();
            healthText.text = "Boss: " + (currentHealth / 15f * 100f).ToString("F0") + "%";
        }

        /// <Summary>
        /// If vulerable, take the hit. 
        /// </Summary>
        void OnTriggerEnter2D(Collider2D other) {
            if(vulerable && other.tag == "Shots") {
                Destroy(other.gameObject);
                hit();
            }
        }

        /// <Summary>
        /// Reduce health, update display. If dead, notify that dead and destroy it. 
        /// </Summary>
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

        /// <summary>
        /// Increase the count of destoryed cannons (to be called by the cannons). Then make vulneable. 
        /// </summary>
        public void cannonHit() {
            bustedCannons++;
            if(bustedCannons > 0 && bustedCannons % 2 == 0) {
                StartCoroutine(HitMe());
            }
        }

        /// <summary>
        /// Makes vulnerable for a given interval and then reloads the cannons. 
        /// </summary>
        IEnumerator HitMe() {
            vulerable = true;
            yield return new WaitForSeconds(timeVulnerable);
            vulerable = false;
            cannonLeft.GetComponent<CannonController>().reloadHealth();
            cannonRight.GetComponent<CannonController>().reloadHealth();
        }
    }
}