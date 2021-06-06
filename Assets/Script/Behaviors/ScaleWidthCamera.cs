using UnityEngine;
[ExecuteInEditMode]
public class ScaleWidthCamera : MonoBehaviour {
	public Camera mainCamera;
	public int targetWidth = 640;
	public float pixelsToUnit = 100;
	// Update is called once per frame
	void Update () {
		int height = Mathf.RoundToInt(targetWidth / (float)Screen.width * Screen.height);
		mainCamera.orthographicSize = height / pixelsToUnit / 4;
	}
}
