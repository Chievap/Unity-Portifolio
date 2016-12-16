using UnityEngine;
using System.Collections;

public class Camera_Controller : MonoBehaviour {
	
	private 	float 		x =0.0f;
	private 	float 		y =0.0f;
	private	 	float 		camDis	= 3 ;
	private 	float 		disiredDis;
	private 	float 		correctedDis;
	private 	float 		curDis;


	private		int 		mouseXSpeedMod = 5;
	private		int 		mouseYSpeedMod = 3;

	public 		Transform 	cameraTarget;

	public 		float 		bodyRotateY;
	public 		float 		bodyRotateX;
	public 		float		bodyRotateMult;
	public 		float 		maxViewDis = 25 ;
	public 		float 		minViewDis = 1 ;
	public 		float 		caracterHeight = 1;

	public 		int 			zoomRate = 30;
	public 		int 			lerpSpeed = 10;


	void Start () {
		Vector3 angels = transform.eulerAngles;
		x = angels.x;
		y = angels.y;

		disiredDis 		= 		camDis;
		correctedDis 	= 		camDis;
		curDis 			= 		camDis;
	}


	void LateUpdate () {
		bodyRotateX = (Input.GetAxis ("Mouse X") * bodyRotateMult);
//		print(bodyRotateY);
//		print(bodyRotateX);
		cameraTarget.Rotate(0, bodyRotateX * Time.deltaTime, 0);

		if (Input.GetMouseButton (0)) {
			x += Input.GetAxis("Mouse X") * mouseXSpeedMod;
			y -= Input.GetAxis("Mouse Y") * mouseYSpeedMod;
		}
		else if(Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0){
			float targetRotAngle = cameraTarget.eulerAngles.y;
			float cameraRotAngle = transform.eulerAngles.y;
			x = Mathf.LerpAngle(cameraRotAngle,targetRotAngle,lerpSpeed * Time.deltaTime);

		}

		y = ClampAngle (y,-50,80);

		Quaternion rotation = Quaternion.Euler (y, x, 0);

		disiredDis -= Input.GetAxis ("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs (disiredDis);
		disiredDis = Mathf.Clamp (disiredDis, minViewDis, maxViewDis);
		correctedDis = disiredDis;

		Vector3 position = cameraTarget.position - (rotation * Vector3.forward * disiredDis);

		RaycastHit colHit;
		Vector3 camTargetPos = new Vector3 (cameraTarget.position.x, cameraTarget.position.y + caracterHeight, cameraTarget.position.z);
		bool isCorrected = false;
		if(Physics.Linecast(camTargetPos, position, out colHit)){
			position = colHit.point;
			correctedDis = Vector3.Distance(camTargetPos , position);
			isCorrected = true;
		}

		curDis = !isCorrected || correctedDis > curDis ? Mathf.Lerp (curDis, correctedDis, Time.deltaTime * zoomRate) : correctedDis;
		position = cameraTarget.position - (rotation * Vector3.forward * curDis + new Vector3(0,-caracterHeight,0));

		transform.rotation = rotation;
		transform.position = position;

	}

	private static float ClampAngle(float angle, float min, float max){
		if (angle < -360) {
			angle += 360;
		}
		if (angle > 360) {
			angle -= 360;
		}
		return Mathf.Clamp (angle, min, max);

	}

}
