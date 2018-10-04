using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mottel {
public class DestoryByShot : MonoBehaviour {
    public GameObject explosion;
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