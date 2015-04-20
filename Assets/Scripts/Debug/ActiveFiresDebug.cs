using UnityEngine;
using System.Collections;

public class ActiveFiresDebug : MonoBehaviour {

	public string OutputStringFormat = "Active Fires: {0}";
	private int _activeFires;

	// Use this for initialization
	void Awake () {
		Messenger<Vector2, FireStage>.AddListener ("fireStarted", OnFireStarted);
		Messenger<Vector2>.AddListener ("fireExtinguished", OnFireExtinguished);
		Messenger<Vector2>.AddListener ("buildingDestroyed", OnBuildingDestroyed);
	}

	void OnFireStarted(Vector2 tile, FireStage stage) {
		_activeFires++;
	}

	void OnFireExtinguished(Vector2 tile) {
		_activeFires--;
	}

	void OnBuildingDestroyed(Vector2 tile) {
		_activeFires--;
	}
}
