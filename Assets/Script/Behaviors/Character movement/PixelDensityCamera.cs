using UnityEngine;

public class PixelDensityCamera : MonoBehaviour {
	public Camera mainCamera;
	public float pixelsToUnits = 100;
	// Update is called once per frame
	void Update () {
		mainCamera.orthographicSize = Screen.height / pixelsToUnits / 2;
	}
}
