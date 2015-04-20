using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Tile))]
public class TileRenderer : BaseMonoBehaviour {
	public Tile Tile;
	private GameObject _currentFireObject;

	void Awake() {
		Tile = GetComponent<Tile> ();
	}

	public void RenderFire(GameObject firePrefab) {
		if (_currentFireObject != null) {
			ClearFire ();
		}

		_currentFireObject = Instantiate (firePrefab);
		
		_currentFireObject.transform.SetParent (transform);
		_currentFireObject.transform.localPosition = Vector2.zero;
	}

	public void ClearFire() {
		if (_currentFireObject == null) {
			return;
		}

		DestroyImmediate (_currentFireObject);
	}

	Vector2 GetVector(int stateIndex, int childrenCount) {
		Vector2[] oneState = new Vector2[] {
			new Vector2(0, 0)
		};

		Vector2[] twoStates = new Vector2[] {
			new Vector2(-0.5f, 0),
			new Vector2(0.5f, 0)
		};

		Vector2[] threeStates = new Vector2[] {
			new Vector2(-0.5f, .5f),
			new Vector2(0.5f, .5f),
			new Vector2(0, -0.5f)
		};

		Vector2[] fourStates = new Vector2[] {
			new Vector2(0, 0),
			new Vector2(0, 0),
			new Vector2(0, 0),
			new Vector2(0, 0)
		};

		switch(childrenCount) {
		case 1:
			return oneState[stateIndex];
		case 2:
			return twoStates[stateIndex];
		case 3:
			return threeStates[stateIndex];
		case 4:
			return fourStates[stateIndex];
		}

		return new Vector2(0, 0);
	}
}