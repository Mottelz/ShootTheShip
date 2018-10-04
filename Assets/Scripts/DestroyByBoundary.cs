using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mottel {
public class DestroyByBoundary : MonoBehaviour{
    void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Shots" && GameState.Instance.mode == GameMode.BulletHell) {
            return;
        } else {
            Destroy(other.gameObject);
        }
    }
}
}