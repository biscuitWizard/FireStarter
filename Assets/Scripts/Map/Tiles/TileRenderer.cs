using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Tile))]
public class TileRenderer : MonoBehaviour {
	public Tile TileInfo;
	private readonly IList<GameObject> _statePrefabs = new List<GameObject> ();
	private readonly IList<TileState> _states = new List<TileState> ();
	private GameObject _root;

	void Awake() {
		_root = new GameObject ("Tile Icons");
		_root.transform.parent = transform;
	}

	public void RenderState(GameObject tileState) {
		_statePrefabs.Add (tileState);
		ClearGraph ();
		RefreshRender ();
	}

	public void RemoveState(GameObject tileState) {
		_statePrefabs.Remove (tileState);
		ClearGraph ();
		RefreshRender ();
	}

	void ClearGraph() {
		foreach (var state in _states) {
			DestroyImmediate(state);
		}

		_states.Clear ();
	}

	void RefreshRender() {
		var index = 0;
		foreach (var statePrefab in _statePrefabs) {
			var state = Instantiate(statePrefab);
			var tileState = state.GetComponent<TileState>();
			_states.Add (tileState);
			state.transform.parent = _root.transform;

			state.name = tileState.StateName;
			state.transform.localPosition = GetVector(index);
			index++;
		}
	}

	Vector2 GetVector(int stateIndex) {

	}
}