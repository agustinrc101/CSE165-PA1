﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookUp : MonoBehaviour {	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.LookRotation(Vector3.up, Vector3.up);
	}
}
