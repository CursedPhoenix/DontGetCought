using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float moveSpeed = 6f;
	public float rotateSpeed = 6f; 
	Vector3 movement;
	Rigidbody playerRigidbody;

	void Awake () {
		playerRigidbody = GetComponent<Rigidbody>();
        playerRigidbody.freezeRotation = true;
	}
	
	void FixedUpdate () {
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		Move (h, v);
		Rotate (h, v);

	}

	void Move (float h, float v) {
		movement.Set(h, 0f, v);
		movement = movement.normalized * moveSpeed * Time.deltaTime;
		playerRigidbody.MovePosition (transform.position + movement);
	}

	void Rotate (float h, float v) {
		if (h != 0 || v != 0 && (Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))) {
			var newRotation = Quaternion.LookRotation (movement);
			var currentRotation = playerRigidbody.rotation;
			if (currentRotation != newRotation) {
				playerRigidbody.rotation = Quaternion.RotateTowards (currentRotation, newRotation, rotateSpeed * Time.deltaTime);
			}
		}
	}
}