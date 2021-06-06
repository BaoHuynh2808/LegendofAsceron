using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalableCamera : MonoBehaviour {
	public Camera mainCamera;
	// Use this for initialization
	void Start () {
		float TARGET_WIDTH = 960.0f;
		float TARGET_HEIGHT = 540.0f;
		int PIXELS_TO_UNITS = 45;
		float desiredRatio = TARGET_WIDTH / TARGET_HEIGHT;
		float currentRatio = (float)Screen.width / (float)Screen.height;
		if (currentRatio >= desiredRatio) {
			mainCamera.orthographicSize = TARGET_HEIGHT / 4 / PIXELS_TO_UNITS;
		}
		else {
			float differenceInSize = desiredRatio / currentRatio;
			mainCamera.orthographicSize = TARGET_HEIGHT / 4 / PIXELS_TO_UNITS * differenceInSize;
		}
	}
}
