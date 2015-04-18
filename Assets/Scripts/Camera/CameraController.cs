using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraController : BaseMonoBehaviour {

	public bool CameraLocked = false;
	private Vector2 _mapTopLeftCorner;
	private Vector2 _mapSize;

	// Use this for initialization
	void Awake () {
		Messenger<bool>.AddListener ("lockCamera", OnLockCamera);
		Messenger.AddListener<bool> ("isCameraLocked", OnIsCameraLocked);
	}

	void Start() {
		Messenger.Broadcast<Vector4> ("getMapBorders", OnGetMapBorders);
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

		transform.Translate (horizontalMovement, verticalMovement, 0);
	}

	void OnGetMapBorders(Vector4 borders) {
		_mapTopLeftCorner = new Vector2 (borders.x, borders.y);
		_mapSize = new Vector2 (borders.w, borders.z);
	}

	void OnLockCamera(bool cameraLock) {
		CameraLocked = cameraLock;
	}

	bool OnIsCameraLocked() {
		return CameraLocked;
	}
}
