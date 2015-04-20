using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer), typeof(TileRenderer), typeof(FlammableTile))]
public class Tile : BaseMonoBehaviour {
	public SpriteRenderer spriteRenderer;
	public TileRenderer tileRenderer;
	public Vector2 Position { get; set; }
	public bool HasBuilding = true;
	public FireStage StartingFireStage = FireStage.Flammable;

	private FlammableTile _flammableTile;

	// Use this for initialization
	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		_flammableTile = GetComponent<FlammableTile> ();
	}

	void Start() {
		SetFire (StartingFireStage);
	}

	public void SetTilePosition(Vector2 position) {
		name = string.Format ("Tile {0},{1}", position.x, position.y);
		Position = position;
	}

	public void SetFire(FireStage severity) {
		Debug.Log ("Set fire command called");
		_flammableTile.StartFire (severity);
	}

	public FireStage GetFireSeverity() {
		return _flammableTile.GetFireStage();
	}

	public bool CanSetOnFire(){
		if (this._flammableTile.GetFireStage() == FireStage.Flammable){
			return true;
		} else {
			return false;
		}
	}

	public bool IsOnFire(){
		if (this._flammableTile.GetFireStage() == FireStage.Kindling 
		    || this._flammableTile.GetFireStage() == FireStage.Hazard 
		    || this._flammableTile.GetFireStage() == FireStage.Raging){
			return true;
		} else {
			return false;
		}
	}

	void OnMouseDown() {
		Messenger<Vector2>.Broadcast<EntityBase> ("placePickle", Position, OnCreatePickle);
	}

	void OnCreatePickle(EntityBase pickle) {

	}
}
