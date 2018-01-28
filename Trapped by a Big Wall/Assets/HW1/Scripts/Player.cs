using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	[SerializeField] private Image progressBar;
	[SerializeField] private Image resetProgressBar;
	[SerializeField] private GameObject teleportLocation;
	[SerializeField] private BrickCylinder brickCylinder;
	[SerializeField] private float lookDistance = 10.0f;
	[SerializeField] private float teleportTime = 5.0f;
	[SerializeField] private float teleportRadius = 1.5f;

	private Transform prevObject;

	private Vector3 teleportCenter;
	private float time;
	private float time2;

	// Use this for initialization
	void Start () {
		//Hides mouse cursor
		//Cursor.lockState = CursorLockMode.Locked;

		teleportCenter = Vector3.zero;
		time = 0;
		time2 = 0;

	}
	
	// Update is called once per frame
	void Update () {
		//check if esc key was pressed to reenable the cursor
		if (Input.GetKeyDown(KeyCode.Escape)) {
			//Cursor.lockState = CursorLockMode.None;
			Application.Quit();
		}

		look();
		checkRotation();
	}

	private void look() {
		Vector3 cameraCenter = new Vector3(0.5f, 0.5f, 0);

		Ray rayOrigin = Camera.main.ViewportPointToRay(cameraCenter);
		RaycastHit hitInfo;

		if (Physics.Raycast(rayOrigin, out hitInfo)) {
			if (prevObject == null)
				prevObject = hitInfo.transform;

			//What is being hit?
			int hitObj = 0;
			if (hitInfo.transform.CompareTag("Brick")) {
				hitObj = 1;
			}
			else if (hitInfo.transform.CompareTag("Ground")) {
				hitObj = 2;
			}

			/******************CHECKING WHAT GOT HIT*******************/
			//Did not hit the ground or a brick
			if (hitObj == 0) {
				reset();
			}
			//Hit a brick
			else if (hitObj == 1) {
				if (hitInfo.transform.GetInstanceID() == prevObject.GetInstanceID()) {
					BrickBehavior brick = prevObject.GetComponent<BrickBehavior>();

					brick.highlight();

					time += Time.deltaTime;

					float maxTime = brick.lookAtTime;

					progressBar.fillAmount = Mathf.Clamp01(time / maxTime);

					if (time > maxTime) {
						Vector3 playerPos = new Vector3(transform.position.x, 0, transform.position.z);
						brick.pushBack(playerPos);
					}

				}
				else {
					reset();
					prevObject = hitInfo.transform;
				}
			}
			//Hit the ground
			else if (hitObj == 2) {
				if (hitInfo.transform.GetInstanceID() == prevObject.GetInstanceID()) {
					Vector3 hitPoint = new Vector3(hitInfo.point.x, 0, hitInfo.point.z);
					teleportLocation.transform.position = hitInfo.point;
					teleportLocation.SetActive(true);

					if (Vector3.Distance(hitPoint, teleportCenter) > teleportRadius) {
						time = 0;
						progressBar.fillAmount = 0;
						teleportCenter = hitPoint;
					}
					else {

						time += Time.deltaTime;
						progressBar.fillAmount = Mathf.Clamp01(time / teleportTime);

						if (time > teleportTime) {
							teleport(hitInfo.point);
							reset();
						}
					}
				}
				else {
					reset();
					prevObject = hitInfo.transform;
				}
			}
		}
		else {
			reset();
		}
	}

	private void teleport(Vector3 newPos) {
		transform.position = new Vector3(newPos.x, transform.position.y, newPos.z);
	}

	private void reset() {
		if (prevObject != null && prevObject.GetComponent<BrickBehavior>() != null)
			prevObject.GetComponent<BrickBehavior>().resetStats();

		time = 0;
		progressBar.fillAmount = 0;
		teleportLocation.SetActive(false);
	}

	private void checkRotation() {
		Vector3 curAngle = transform.GetChild(0).transform.localEulerAngles;

		float mod = curAngle.x % 360;

		//Limits up/down mobility
		if (mod <= 45 || mod >= 300) {
			time2 = 0;
			resetProgressBar.fillAmount = 0;
			resetProgressBar.gameObject.SetActive(false);
		}
		else if (mod < 300 && mod >= 270) {
			resetProgressBar.gameObject.SetActive(true);
			time2 += Time.deltaTime;
			resetProgressBar.fillAmount = Mathf.Clamp01(time2 / 2.0f);

			if (time2 >= 2.0f) {
				brickCylinder.resetCylinder();
				time2 = 0;
			}

		}
		else {
			time2 = 0;
			resetProgressBar.fillAmount = 0;
			resetProgressBar.gameObject.SetActive(false);
		}
	}
}
