using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class PixelPerfectCamera : MonoBehaviour {

	public float PixelsToUnits = 100f;
	public float Zoom = 1;
	private Camera _camera;
	private float _currentZoom;

	// Use this for initialization
	void Start () {
		_camera = GetComponent<Camera> ();

		SetCameraZoom (Zoom);
	}

	void SetCameraZoom(float newZoom) {
		var unitsPerPixel = 1f / (PixelsToUnits * Zoom);
		
		_camera.orthographicSize = (Screen.height / 2f) * unitsPerPixel;

		_currentZoom = newZoom;
	}
}
