using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mottel {
    
	/// <summary>
    /// Moves all of the non-player objects using physics and RigidBody2D.
    /// </summary>
    public class MoveByRB2D : MonoBehaviour {
		public float speed;
		public MovementType movement;
		public Boundry bounds;
        private Rigidbody2D rb;
		private float startTime;
        
		/// <summary>
        /// Sets the simple velocity, based on the selected MovementType or calls ComplexMovement().
        /// </summary>
        private void Start() {
            startTime = Time.fixedTime;
			rb = GetComponent<Rigidbody2D>();
			if(movement == MovementType.Down) {
				rb.velocity = Vector2.down * speed;
			} else if(movement == MovementType.Up) {
				rb.velocity = Vector2.up * speed;
			} else if(movement == MovementType.LeftBullet) {
				rb.velocity = new Vector2(0.13f, 1.0f) * speed;
			} else if(movement == MovementType.RightBullet) {
				rb.velocity = new Vector2(-0.13f, 1.0f) * speed;
			} else if (movement == MovementType.Boss) {
                StartCoroutine(ComplexMovement());
			} else if (movement == MovementType.Centipede) { 
				rb.velocity = Vector2.down;
			}
		}

		private void FixedUpdate() {
			float eslapsed = Time.fixedTime - startTime;
            if (movement == MovementType.Centipede) {
				rb.AddForce(Vector2.left * Mathf.Cos(eslapsed/0.85f) * speed, ForceMode2D.Force);
            }
		}

        /// <summary>
        /// Complex movement that requires pauses over time. Decided based on some basic math.
        /// </summary>
        IEnumerator ComplexMovement() {
			rb.velocity = Vector2.down * speed;
			yield return new WaitForSeconds(1.5f);
			while(true) {
				//move left
				rb.velocity = Vector2.left * speed;
				float time = Mathf.Abs(transform.position.x - bounds.xMin) / speed;
				yield return new WaitForSeconds(time);
				//move right
				rb.velocity = Vector2.right * speed;
				time = Mathf.Abs(transform.position.x - bounds.xMax) / speed;
				yield return new WaitForSeconds(time);
			}
		}
	}
}