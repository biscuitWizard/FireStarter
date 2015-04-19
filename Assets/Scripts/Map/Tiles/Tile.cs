using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour {
	public SpriteRenderer spriteRenderer;
	public Vector2 Position { get; private set; }
	public bool HasBuilding = true;
	public FireStage StartingFireStage = FireStage.Flammable;
	private FireStage _fireSeverity;

	// Use this for initialization
	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer> ();

		SetFire (StartingFireStage);
	}

	public void SetTilePosition(Vector2 position) {
		name = string.Format ("Tile {0},{1}", position.x, position.y);
		Position = position;
	}

	public void SetFire(FireStage severity) {
		_fireSeverity = severity;

		if (severity == FireStage.Flammable
			|| severity == FireStage.NotFlammable) {

			// Clear graphics for fire.
			return;
		}

		// Set graphics for fire level.
	}

	public FireStage GetFireSeverity() {
		return _fireSeverity;
	}

	public bool CanSetOnFire(){
		if (this._fireSeverity == FireStage.Flammable){
			return true;
		} else {
			return false;
		}
	}

	public bool IsOnFire(){
		if (this._fireSeverity == FireStage.Kindling || this._fireSeverity == FireStage.Hazard || this._fireSeverity == FireStage.Raging){
			return true;
		} else {
			return false;
		}
	}
}
