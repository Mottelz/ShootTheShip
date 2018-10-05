using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mottel {
    /// <summary>
    /// Controls the player.
    /// </summary>
    public class PlayerController : MonoBehaviour {
        public GameObject explosion, cannon, shot, shotLeft, shotRight;
        public float speed, cooldown, bulletOffset; 
        public Boundry boundry;
        public int health, maxHealth;
        private float nextShot;
        public Text healthText;

        /// <summary>
        /// On launch, set the Player's Health HUD.
        /// </summary>
        private void Start() {
            healthText.text = "Health: " + health;
        }

        /// <summary>
        /// Fire based on the health and update the cooldown timer.
        /// </summary>
        private void Update () {
            if (Input.GetButton("Jump") && Time.time > nextShot){
                if(health == 3 ){
                    Instantiate(shotLeft, cannon.transform.position, cannon.transform.rotation);
                    Instantiate(shot, cannon.transform.position, cannon.transform.rotation);
                    Instantiate(shotRight, cannon.transform.position, cannon.transform.rotation);
                } else if (health == 2) {
                    Instantiate(shot, new Vector3(cannon.transform.position.x + bulletOffset, cannon.transform.position.y, cannon.transform.position.z), cannon.transform.rotation);
                    Instantiate(shot, new Vector3(cannon.transform.position.x - bulletOffset, cannon.transform.position.y, cannon.transform.position.z), cannon.transform.rotation);
                } else {
                    Instantiate(shot, cannon.transform.position, cannon.transform.rotation);
                }
                nextShot = Time.time + cooldown;
            }

        }

        /// <summary>
        /// Move the player using transform.postion based on input.
        /// </summary>
        private void FixedUpdate() {
            //Move
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
            transform.position += movement * speed * Time.deltaTime;
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, boundry.xMin, boundry.xMax),
                Mathf.Clamp(transform.position.y, boundry.yMin, boundry.yMax),
                0.0f);
        }

        /// <summary>
        /// Collider to update player if we get hit by a shot, enemy or upgrade.
        /// </summary>
        void OnTriggerEnter2D(Collider2D other){
            if (other.tag == "Boundary") {
                return;
            } else if(other.tag == "Shots" || other.tag == "Enemies") {
                Destroy(other.gameObject);
                hit();
            } else if(other.tag == "Powerup") {
                Destroy(other.gameObject);
                upgradeHealth();
            }
        }

        /// <summary>
        /// Take a hit, update health, create explosion, destroy if dead.
        /// </summary>
        public void hit(){
            health--;
            healthText.text = "Health: " + health;
            Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z -2), transform.rotation);
            if (health < 1) {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Update health, clamp between max/min health.
        /// </summary>
        public void upgradeHealth() {
            health++;
            Mathf.Clamp(health, 0, maxHealth);
            healthText.text = "Health: " + health;
        }
    }
}