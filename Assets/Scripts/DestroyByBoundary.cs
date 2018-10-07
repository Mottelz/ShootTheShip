using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mottel {
    /// <summary>
    /// Attch to Boundary, destorys anything that leaves.
    /// </summary>
    public class DestroyByBoundary : MonoBehaviour{
        /// <summary>
        /// Will destory everything besides shots while in bullet mode.
        /// </summary>
        void OnTriggerExit2D(Collider2D other) {
            if((other.tag == "Shots" || other.tag == "Player Shots") && GameState.Instance.mode == GameMode.BulletHell) {
                return;
            } else {
                Destroy(other.gameObject);
            }
        }
    }
}