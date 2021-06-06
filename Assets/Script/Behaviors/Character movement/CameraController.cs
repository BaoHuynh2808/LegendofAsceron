using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	//public Transform target;
	//public float lookSmooth = 0.09f;
	//public Vector3 offsetFromTarget = new Vector3(0, 6, -8);
	//public float xTilt = 10;
	//private Vector3 destination = Vector3.zero;
	//private PlayerMovement playerMovement;
	//private float rotateVel = 0;
	//Vector3 cameraPosition = new Vector3(0,6.0f,-3.0f);

	//void SetCameraTarget(Transform t) {
	//target = t;
	//if (target.GetComponent<PlayerMovement>()) {
	//	playerMovement = target.GetComponent<PlayerMovement>();
	//}
	//}
    

	private void LateUpdate() {
		//yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
		//pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
		//pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
		//yaw = Mathf.Clamp(yaw, yawMinMax.x, yawMinMax.y);
		//Vector3 targetRotaion = new Vector3(pitch, yaw);
		//currentRotation = Vector3.SmoothDamp(currentRotation, targetRotaion, ref rotationSmoothVelocity, rotationSmoothTime);
		//transform.eulerAngles = currentRotation;
		//transform.position = target.position - transform.forward * distanceFromTarget;
		////MoveToTheTarget();
		////LookAtTarget();
		transform.position = new Vector3(PlayerMovement.instance.transform.position.x, 6.0f, PlayerMovement.instance.transform.position.z - 3.0f);
	}

	//void MoveToTheTarget() {
	//	destination = playerMovement.TargetRotation * offsetFromTarget;
	//	destination += target.position;
	//	transform.position = destination;
	//}

	//void LookAtTarget() {
	//	float eulerYAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target.eulerAngles.y, ref rotateVel, lookSmooth);
	//	transform.rotation = Quaternion.Euler(transform.eulerAngles.x, eulerYAngle, 0);
	//}
}
