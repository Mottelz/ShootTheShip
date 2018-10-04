using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mottel {
public class SimpleShooter : MonoBehaviour {
    private float nextShot;
    public float cooldown;
    public GameObject cannon;
    public GameObject shot;

    private void Update() {
        //Shoot
        if (Time.time > nextShot){
            Instantiate(shot, cannon.transform.position, cannon.transform.rotation);
            nextShot = Time.time + cooldown + (Random.value*cooldown);
        }    
    }

}
}