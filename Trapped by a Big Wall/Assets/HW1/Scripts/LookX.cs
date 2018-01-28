using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookX : MonoBehaviour {
	[SerializeField]
	private float sensitivity = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float mouseX = Input.GetAxis("Mouse X");
		Vector3 curAngle = transform.localEulerAngles;
		mouseX *= sensitivity;

		curAngle.y += mouseX;	//Rotate left/up

		transform.localEulerAngles = curAngle;
		
	}
}
