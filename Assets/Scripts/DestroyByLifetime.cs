using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mottel {
    /// <summary>
    /// Destroys the attached GameObject after a given interval. Mainly used on explosion animations.
    /// </summary>
    public class DestroyByLifetime : MonoBehaviour {
        public float lifetime;
        void Start(){
            Destroy(gameObject, lifetime);
        }
    }
}