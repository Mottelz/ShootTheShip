using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mottel {
public class DestroyByLifetime : MonoBehaviour {
    public float lifetime;
    void Start(){
        Destroy(gameObject, lifetime);
    }
}
}