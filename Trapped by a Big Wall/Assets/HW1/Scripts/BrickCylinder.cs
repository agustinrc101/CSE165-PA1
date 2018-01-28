using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickCylinder : MonoBehaviour {
	[Tooltip("How many bricks are stacked on top of each other")]
	[SerializeField] private int height = 25;
	[SerializeField] private float radius = 3;
	[Tooltip("Gap between two bricks next to each other")]
	[SerializeField] private float brickLength = 1;
	[Tooltip("Gap between two bricks one which is above the other")]
	[SerializeField] private float brickHeight = 0.201f;
	[SerializeField] private float brickStartingPos = -0.4f;
	[SerializeField] private float[] zPosOffest = {-0.2f, 0.2f };
	[SerializeField] private GameObject brickPrefab;

	private Vector3 center;

	// Use this for initialization
	void Start () {
		center = transform.position;

		createBrickCylinder();
		transform.GetChild(0).rotation = Quaternion.identity;
	}

	private void createBrickCylinder(){
		if (transform.childCount < 1) {
			Debug.LogWarning(name + " has no children");
			return;
		}

		Transform rotationObj = transform.GetChild(0);

		float circumference = radius * 2 * Mathf.PI;
		int numObjects = (int)(circumference / brickLength);

		float angleOffset = brickLength / radius;
		angleOffset = (angleOffset * 180) / Mathf.PI;

		for (int h = 0; h < height; h++) {
			Vector3 pos = randomCircle(h * brickHeight);

			float lookX = pos.x - center.x; // center.x - pos.x;
			float lookZ = pos.z - center.z; // center.z - pos.z;

			Quaternion rot = Quaternion.LookRotation(new Vector3(lookX, 0, lookZ));

			for (int i = 0; i < numObjects; i++) {
				Vector3 newPos = new Vector3(pos.x, pos.y, pos.z + Random.Range(zPosOffest[0], zPosOffest[1]));
				Instantiate(brickPrefab, newPos, rot, rotationObj);
				rotationObj.Rotate(new Vector3(0, angleOffset, 0));
			}
		}
	}

	private Vector3 randomCircle(float heightOffset) {
		float angle = Random.Range(0, 360);
		Vector3 pos;
		pos.x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
		pos.y = brickStartingPos + heightOffset;
		pos.z = center.z + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
		return pos;
	}

	public void resetCylinder(){
		if (transform.GetChild(0).transform.childCount == 0)
			return;
		else{
			for (int i = 0; i < transform.GetChild(0).transform.childCount; i++){
				if (i > transform.GetChild(0).transform.childCount) { Debug.Log("less children (brickcylinder.cs)"); }
				DestroyObject(transform.GetChild(0).transform.GetChild(i).gameObject);
			}
		}

		createBrickCylinder();
	}
}
