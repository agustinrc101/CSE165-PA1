using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tpIndicatorBehavior : MonoBehaviour {
	[SerializeField] private float timeToChange = 0.1f;
	[SerializeField] private float minAlpha = 0.2f;
	[SerializeField] private float maxAlpha = 0.6f;
	[SerializeField] private float rateOfChange = 0.01f;

	private Material mat;
	private float curTime;
	private bool add;

	void Start() {
		curTime = 0;
		add = true;

		mat = GetComponent<MeshRenderer>().materials[0];
	}

	void Update () {
		curTime += Time.deltaTime;
		if (curTime > timeToChange) {
			if (add) {
				Color col = mat.color;
				mat.color = new Color(col.r, col.g, col.b, col.a + rateOfChange);

				if (col.a + rateOfChange > maxAlpha)
					add = false;
			}
			else{
				Color col = mat.color;
				mat.color = new Color(col.r, col.g, col.b, col.a - rateOfChange);

				if (col.a - rateOfChange < minAlpha)
					add = true;
			}
		}
	}
}
