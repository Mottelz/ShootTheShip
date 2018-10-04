using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mottel {
public class CannonController : MonoBehaviour {
	public GameObject ship;
	public GameObject barrel;
	public int cooldown;
	public GameObject shot;
    public GameObject explosion;
	public int health;
	private float nextShot;

    private void Update() {
        //Shoot
        if (Time.time > nextShot && health > 0) {
            Instantiate(shot, barrel.transform.position, barrel.transform.rotation);
            nextShot = Time.time + cooldown + (Random.value * cooldown);
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Boundary"){
            return;
        } else if (other.tag == "Shots" && health > 0) {
            Destroy(other.gameObject);
            hit();
        }
    }

	void hit() {
        Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z - 2), transform.rotation);
        health--;
        if(health < 1) {
            ship.GetComponent<BossController>().cannonHit();
		}
	}

	public void reloadHealth() {
		health = 2;
	}
}
}