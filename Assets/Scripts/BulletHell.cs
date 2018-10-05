using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mottel {
    
    /// <summary>
    /// Attached to the bullets/lasers so bullet hell mode can be activated.
    /// </summary>
    public class BulletHell : MonoBehaviour {
        public Boundry bounds;
        public int health;
        /// <summary>
        /// If you leave the boundary multiply that coordinate by -1. If BulletHell mode is not set in GameState, the boundary will destory the bullet and this script will never take effect. 
        /// </summary>
        private void OnTriggerExit2D(Collider2D other) {
            if (other.tag == "Boundary"){
				health--;
				if(health < 1){
					Destroy(gameObject);
				} else if (transform.position.x > bounds.xMax || transform.position.x < bounds.xMin) {
                    transform.position = new Vector3(transform.position.x * -1, transform.position.y, transform.position.z);
                } else if(transform.position.y > bounds.yMax || transform.position.y < bounds.yMin) {
                    transform.position = new Vector3(transform.position.x, transform.position.y * -1, transform.position.z);
				}
            }
        }
    }
}
