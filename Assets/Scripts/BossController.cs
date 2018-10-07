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
        public GameObject explosion, deathExpolsion, cannonLeft, cannonRight;
        private Text healthText;
        private SpriteRenderer sr;
        
        /// <Summary>
        /// Set health, grab text and set it. 
        /// </Summary>
        private void Start() {
            currentHealth = health;
            healthText = GameObject.Find("Boss Health").GetComponent<Text>();
            healthText.text = "Boss: " + (currentHealth / 15f * 100f).ToString("F0") + "%";
            sr = GetComponent<SpriteRenderer>();
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
            healthText.text = "Boss: " + Mathf.Clamp((currentHealth / 15f * 100f), 0f, 100f).ToString("F0") + "%";
            Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z - 2), transform.rotation);
            if (currentHealth < 1) {
                Instantiate(deathExpolsion, transform.position, transform.rotation);
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
            StartCoroutine(smoothTrans(1f, 0.5f));
            yield return new WaitForSeconds(timeVulnerable);
            vulerable = false;
            StartCoroutine(smoothTrans(0.5f, 0.25f));
            cannonLeft.GetComponent<CannonController>().reloadHealth();
            cannonRight.GetComponent<CannonController>().reloadHealth();
        }

        /// <summary>
        /// A smooth transition for opacity using lerp and fixedupdate.
        /// </summary>
        IEnumerator smoothTrans(float end, float duration) {
            float elapsed = 0f;
            float start = sr.color.a;
            while (sr.color.a != end) {
                float newVal = Mathf.Lerp(start, end, (elapsed / duration));
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, newVal);
                elapsed += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}