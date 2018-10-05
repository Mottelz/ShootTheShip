using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mottel {
    /// <summary>
    /// Makes the background move. Gets the speed and duration of the animation from user.
    /// </summary>
    public class BackgroundScroller : MonoBehaviour {

    public float scrollSpeed, stopAfter;
    private Vector2 savedOffset;
	private Renderer rend;

    /// <summary>
    /// Loads the rendered and starts the timer
    /// </summary>
    void Start() {
    rend = GetComponent<Renderer>();
    savedOffset = rend.sharedMaterial.GetTextureOffset("_MainTex");
    StartCoroutine(DelayedEnd());
    }

    /// <summary>
    /// Disables script to stop the texture. 
    /// </summary>
    void Update() {
    float y = Mathf.Repeat(Time.time * scrollSpeed, 1);
    Vector2 offset = new Vector2(savedOffset.x, y);
    rend.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }

    /// <summary>
    /// Disables script to stop the texture. 
    /// </summary>
    IEnumerator DelayedEnd() {
    yield return new WaitForSeconds(stopAfter);
    this.enabled = false;
	}
}
}