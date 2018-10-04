﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mottel {
	[System.Serializable]
	public enum MovementType { Up, Down, Centipede, Boss, LeftBullet, RightBullet };

	public class MoveByRB2D : MonoBehaviour {
		public float speed;
		public MovementType movement;
		private Rigidbody2D rb;
		public Boundry bounds;

		private void Start() {
			rb = GetComponent<Rigidbody2D>();
			if(movement == MovementType.Down) {
				rb.velocity = Vector2.down * speed;
			} else if(movement == MovementType.Up) {
				rb.velocity = Vector2.up * speed;
			} else if(movement == MovementType.LeftBullet) {
				rb.velocity = new Vector2(0.13f, 1.0f) * speed;
			} else if(movement == MovementType.RightBullet) {
				rb.velocity = new Vector2(-0.13f, 1.0f) * speed;
			} else {
				StartCoroutine(ComplexMovement());
			}
		}

		IEnumerator ComplexMovement() {
			if(movement == MovementType.Centipede) {
				while(true) {
					//move left
					rb.velocity = Vector2.left * speed;
					float time = Mathf.Abs(transform.position.x - bounds.xMin) / speed;
					yield return new WaitForSeconds(time);
					//move down
					rb.velocity = Vector2.down * speed;
					yield return new WaitForSeconds(0.3f);
					//move right
					rb.velocity = Vector2.right * speed;
					time = Mathf.Abs(transform.position.x - bounds.xMax) / speed;
					yield return new WaitForSeconds(time);
					//move down
					rb.velocity = Vector2.down * speed;
					yield return new WaitForSeconds(0.3f);
				}
			} else if (movement == MovementType.Boss) {
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
}