using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour {
	public float bobSpeed;
    public float bobHeight;

	private float initialHeight;
    private float timerAngle;
	private float wavePos;
	private CharacterController player;

	void Start() {
		timerAngle = 0f;
		initialHeight = Camera.main.transform.localPosition.y;
		player = GetComponent<CharacterController>();
	}

    void Update() {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		bool playerMoving = moveHorizontal != 0f || moveVertical != 0f;

		if (playerMoving && player.isGrounded) {
			// Finds the position on the wave that goes from 0 to 1 to 0.
			wavePos = (Mathf.Cos(timerAngle) / -2f) + 0.5f;
			// Increases timerAngle and makes sure it's less than 2Pi.
			timerAngle = timerAngle + (bobSpeed * PlayerMovement.movementSpeed * Time.deltaTime);

			if (timerAngle > Mathf.PI * 2f) {
				timerAngle = timerAngle - (Mathf.PI * 2f);
			}
		}
		/*
		else {
			// This resets the camera to it's initial position.
			if (wavePos > 0) {
				wavePos = wavePos - 0.005f;
			}
			else {
				wavePos = 0f;
				// Prevents head from jerking upwards if the player walks
				// before it returns to original position.
				timerAngle = 0f;
			}
		}
		*/

		Vector3 movement = Camera.main.transform.localPosition;

		if (wavePos > 0f) {
			float heightIncrease = wavePos * bobHeight;
			movement.y = initialHeight + heightIncrease;
		}
		else {
			movement.y = initialHeight;
		}

		Camera.main.transform.localPosition = movement;
    }
}
