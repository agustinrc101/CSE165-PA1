using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookY : MonoBehaviour {
	[SerializeField] private float sensitivity = 1.0f;
	[SerializeField] private Image progressBar;
	[SerializeField] private BrickCylinder brickCylinder;

	private float time;

	// Update is called once per frame
	void Update() {
		float mouseY = Input.GetAxis("Mouse Y");
		Vector3 curAngle = transform.localEulerAngles;
		mouseY *= sensitivity;

		curAngle.x -= mouseY;   //Rotate down/up
		
		float mod = curAngle.x % 360;
		transform.localEulerAngles = curAngle;

		//Limits up/down mobility
		if (mod <= 45 || mod >= 300) {
			//transform.localEulerAngles = curAngle;
			time = 0;
			progressBar.fillAmount = 0;
			progressBar.gameObject.SetActive(false);
		}
		else if (mod < 300 && mod >= 270) {
			progressBar.gameObject.SetActive(true);
			time += Time.deltaTime;
			progressBar.fillAmount = Mathf.Clamp01(time / 5.0f);

			if (time >= 5.0f) {
				brickCylinder.resetCylinder();
				time = 0;
			}

		}
		else {
			time = 0;
			progressBar.fillAmount = 0;
			progressBar.gameObject.SetActive(false);
		}
	}
}
