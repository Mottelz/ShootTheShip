using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mottel {
    /// <summary>
    /// Simple script that fires a given bullet at a random time after a set interval.
    /// </summary>
    public class SimpleShooter : MonoBehaviour {
        private float nextShot;
        public float cooldown;
        public GameObject cannon, shot;

        /// <summary>
        /// If you can, fire the cannon.
        /// </summary>
        private void Update() {
        //Shoot
        if (Time.time > nextShot){
            Instantiate(shot, cannon.transform.position, cannon.transform.rotation);
            nextShot = Time.time + cooldown + (Random.value*cooldown);
        }    
    }

    }
}