using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBehavior : MonoBehaviour {
	[SerializeField] private float speed = 0.5f;
	[SerializeField] public float lookAtTime = 2.0f;
	[SerializeField] private Color lookedAtColor;
	[Tooltip("True means bricks are pushed outwards only, False takes player position into consideration")]
	[SerializeField] private bool simplePushing = false;

	private Rigidbody body;
	private Material mat;
	private Color originalColor;

	// Use this for initialization
	void Start () {
		MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

		if (meshRenderer == null)
			Debug.LogWarning(name + " does not have a MeshRenderer component");

		if (meshRenderer.materials.Length < 1)
			Debug.LogWarning(name + " does not have a material in the MeshRenderer component");
		else
			mat = meshRenderer.materials[0];

		originalColor = mat.color;

		body = GetComponent<Rigidbody>();

		if (body == null)
			Debug.LogWarning(name + " does not have a Rigidbody component");

		resetStats();
	}

	public void highlight() {
		mat.color = lookedAtColor;
	}

	public void pushBack(Vector3 playerPos) {
		if (simplePushing)
			transform.Translate(Vector3.forward * speed * Time.deltaTime);
		else {
			Vector3 direction = new Vector3(transform.position.x, 0, transform.position.z);

			direction = direction - playerPos;                  //playerPos = some vector3 (x, 0, z)

			Vector3 velocity = direction * speed;               //velocity = speed * direction

			transform.Translate(velocity * Time.deltaTime, Space.World);
		}
	}

	public void resetStats() {
		mat.color = originalColor;
	}
}
