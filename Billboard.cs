using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {
	private GameObject player;

	void Start() {
		player = GameObject.Find("Player");
	}

	void Update() {
		float rotateY = player.transform.eulerAngles.y;
		Vector3 newRotation = new Vector3(0, rotateY, 0);
		transform.eulerAngles = newRotation;
	}
}
