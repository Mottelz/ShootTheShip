using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mottel {
	public class BGController : MonoBehaviour {
		public float startScrollSpeed, stopAfter, slowDuration;
		private Renderer rend;
		private float scrollSpeed, slowTime;
		private bool slowDown;

        /// <summary>
        /// Loads the renderer, waits, then tells the Update loop to slow.
        /// </summary>
        IEnumerator Start() {
            slowDown = false;
			scrollSpeed = startScrollSpeed;
			rend = GetComponent<Renderer>();
            yield return new WaitForSeconds(stopAfter);
            slowDown = true;
            slowTime = Time.time;
		}

        /// <summary>
        /// Moves background using offset then reduces the speed once slowDown is set to true.
        /// </summary>
        void Update() {
			if(slowDown) {
                scrollSpeed = Mathf.Lerp(startScrollSpeed, 0f, ((Time.time - slowTime) / slowDuration));
			}
            Vector2 offset = rend.sharedMaterial.GetTextureOffset("_MainTex");
            offset.y = Mathf.Repeat(offset.y + scrollSpeed, 1f);
            rend.sharedMaterial.SetTextureOffset("_MainTex", offset);
		}
	}
}