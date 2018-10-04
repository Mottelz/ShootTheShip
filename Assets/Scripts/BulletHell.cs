using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mottel {
    public class BulletHell : MonoBehaviour {
        public Boundry bounds;
        public int health;

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
