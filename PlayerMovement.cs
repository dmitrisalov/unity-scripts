using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	private float VERTICAL_ROT_LIMIT = 90f;

	public float walkSpeed;
	public float sprintSpeed;
	public float mouseSensitivity;
	public float jumpStrength;

	public static float movementSpeed;
	private float moveForBack;
	private float moveLeftRight;
	private float moveVertical;

	private float rotateX;
	private float rotateY;

	private Vector3 movement;
	private CharacterController player;

	// Use this for initialization
	void Start () {
		movementSpeed = walkSpeed;
		rotateY = 0f;
		moveVertical = 0f;

		// Get the character controller of the object that's being moved.
		player = GetComponent<CharacterController>();
		// Makes the mouse pointer invisible.
		Cursor.visible = false;
		// Centers the mouse on screen.
		Cursor.lockState = CursorLockMode.Locked;
	}

	// Update is called once per frame
	void Update () {
		// Set rotation.
		rotateX = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
		rotateY -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

		// Limit vertical rotation.
		if (rotateY > VERTICAL_ROT_LIMIT) {
			rotateY = VERTICAL_ROT_LIMIT;
		}
		else if (rotateY < VERTICAL_ROT_LIMIT * -1) {
			rotateY = VERTICAL_ROT_LIMIT * -1;
		}

		// Rotate the player/camera.
		transform.Rotate(0, rotateX, 0);
		Camera.main.transform.localRotation = Quaternion.Euler(rotateY, 0, 0);

		// Make gravity act on the player.
		moveVertical += Physics.gravity.y * Time.deltaTime;

		// Check if player jumps.
		if (Input.GetButton("Jump") && player.isGrounded) {
			moveVertical = jumpStrength;
		}

		if (player.isGrounded) {
			if (Input.GetKey(KeyCode.LeftShift)) {
				movementSpeed = sprintSpeed;
			}
			else {
				movementSpeed = walkSpeed;
			}
		}
		/*
		// Switch between walk and sprint speed when appropriate.
		if (Input.GetKeyDown(KeyCode.LeftShift) && player.isGrounded) {
			movementSpeed = sprintSpeed;
		}

		if (Input.GetKeyUp(KeyCode.LeftShift)) {
			movementSpeed = walkSpeed;
		}
		*/

		// Set movement direction/magnitude.
		moveForBack = Input.GetAxisRaw("Vertical") * movementSpeed;
		moveLeftRight = Input.GetAxisRaw("Horizontal") * movementSpeed;

		// Move the player.
		movement = new Vector3(moveLeftRight, moveVertical, moveForBack);
		movement = transform.rotation * movement;
		player.Move(movement * Time.deltaTime);
	}
}
