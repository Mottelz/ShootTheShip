using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mottel {
public class BackgroundScroller : MonoBehaviour {

    public float scrollSpeed, stopAfter;
    private Vector2 savedOffset;
	private Renderer rend;

    void Start() {
		rend = GetComponent<Renderer>();
        savedOffset = rend.sharedMaterial.GetTextureOffset("_MainTex");
		StartCoroutine(DelayedEnd());
    }

    void Update() {
        float y = Mathf.Repeat(Time.time * scrollSpeed, 1);
        Vector2 offset = new Vector2(savedOffset.x, y);
        rend.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
    
	IEnumerator DelayedEnd() {
        yield return new WaitForSeconds(stopAfter);
		this.enabled = false;
	}
}
}