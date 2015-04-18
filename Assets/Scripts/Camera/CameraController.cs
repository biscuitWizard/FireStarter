using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera), typeof(PixelPerfectCamera))]
public class CameraController : BaseMonoBehaviour {

	public PixelPerfectCamera ZoomController;
	public bool CameraLocked = false;
	public MapModule BoundMap;
	public float ScrollSensitivity = 0.25f;
	public float MaxZoom = 0.1f;
	public float MinZoom = 0.5f;

	// Use this for initialization
	void Awake () {
		Messenger<bool>.AddListener ("lockCamera", OnLockCamera);
		Messenger.AddListener<bool> ("isCameraLocked", OnIsCameraLocked);
		Messenger<Vector2, float>.AddListener ("cameraPanToAndWait", OnCameraPanToAndWait);
	}
	
	// Update is called once per frame
	void Update () {
		float horizontalMovement;
		float verticalMovement;

		// Do mouse input or use joystick
		// not both
		if (Input.GetMouseButton (0)) {
			horizontalMovement = Input.GetAxis ("Mouse X") * -1;
			verticalMovement = Input.GetAxis ("Mouse Y") * -1;
		} else {
			horizontalMovement = Input.GetAxis ("Horizontal");
			verticalMovement = Input.GetAxis ("Vertical");
		}

		if (Input.GetAxis ("Mouse ScrollWheel") != 0) {
			var zoomMovement = Input.GetAxis ("Mouse ScrollWheel") > 0 
				    ? ScrollSensitivity * Time.deltaTime
					: ScrollSensitivity * Time.deltaTime * -1;
			if(ZoomController.Zoom < MaxZoom
			   && ZoomController.Zoom > MinZoom) {
				ZoomController.SetCameraZoom (ZoomController.Zoom + zoomMovement);
			}
		}

		transform.Translate (horizontalMovement, verticalMovement, 0);
	}

	void OnLockCamera(bool cameraLock) {
		CameraLocked = cameraLock;
	}

	bool OnIsCameraLocked() {
		return CameraLocked;
	}

	void OnCameraPanToAndWait(Vector2 tile, float seconds) {

	}
}
