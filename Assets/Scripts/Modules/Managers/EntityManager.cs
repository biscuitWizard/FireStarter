﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(GameModule))]
public class EntityManager : BaseMonoBehaviour {
	public MapModule Map;
	public GameModule Game;

	public GameObject GoblinPrefab;
	public GameObject WatchmanPrefab;
	public GameObject PicklePrefab;
	public GameObject FiremanPrefab;

	private readonly IList<EntityBase> _goblins = new List<EntityBase>();
	private readonly IList<EntityBase> _watchmen = new List<EntityBase>();
	private readonly IList<EntityBase> _pickles = new List<EntityBase>();
	private readonly IList<EntityBase> _firemen = new List<EntityBase>();

	void Awake() {
		Messenger<Vector2>.AddListener<EntityBase> ("placePickle", CreatePickle);
		Messenger<Vector2>.AddListener<EntityBase> ("placeGoblin", CreateGoblin);
		Messenger<Vector2>.AddListener<EntityBase> ("placeFireman", CreateFireman);
		Messenger<Vector2>.AddListener<EntityBase> ("placeWatchman", CreateWatchman);
	}

	public List<Vector2> getLegalMoves(Vector2 currentPosition){

		List<Vector2> legalMoves = new List<Vector2> ();
		var mapSize = Game.GetMapSize();

		if (currentPosition.x != 0){
			legalMoves.Add (new Vector2 (currentPosition.x-1, currentPosition.y));
		}
		if (currentPosition.x != mapSize.x-1){
			legalMoves.Add (new Vector2 (currentPosition.x+1, currentPosition.y));
		}
		if (currentPosition.y != 0){
			legalMoves.Add (new Vector2 (currentPosition.x, currentPosition.y-1));
		}
		if (currentPosition.y != mapSize.y-1){
			legalMoves.Add (new Vector2 (currentPosition.x, currentPosition.y+1));
		}

		return legalMoves;
	}

	public void MoveEntityTo(EntityBase entity, Vector2 newPosition) {
		var tile = Map.GetTile ((int)newPosition.x, (int)newPosition.y);
		entity.transform.SetParent (tile.transform);
		entity.transform.localPosition = Vector2.zero;

		//entity.SetLocation (newPosition);
	}

	public EntityBase CreateGoblin(Vector2 position) {
		var goblin = Instantiate (GoblinPrefab);
		var goblinEntity = goblin.GetComponent<GoblinEntity> ();

		_goblins.Add (goblinEntity);
		MoveEntityTo (goblinEntity, position);

		return goblinEntity;
	}

	public EntityBase CreateWatchman(Vector2 position) {
		var watchman = Instantiate (WatchmanPrefab);
		var watchmanEntity = watchman.GetComponent<WatchmanEntity> ();

		_watchmen.Add (watchmanEntity);
		MoveEntityTo (watchmanEntity, position);

		return watchmanEntity;
	}

	public EntityBase CreatePickle(Vector2 position) {
		var pickle = Instantiate (PicklePrefab);
		var pickleEntity = pickle.GetComponent<PickleEntity> ();
		
		_pickles.Add (pickleEntity);
		MoveEntityTo (pickleEntity, position);
		
		return pickleEntity;
	}

	public EntityBase CreateFireman(Vector2 position) {
		var fireman = Instantiate (WatchmanPrefab);
		var firemanEntity = fireman.GetComponent<FiremanEntity> ();
		
		_firemen.Add (firemanEntity);
		MoveEntityTo (firemanEntity, position);
		
		return firemanEntity;
	}

	public EntityBase[] GetPickles() {
		return _pickles.ToArray ();
	}

	public EntityBase[] GetFiremen() {
		return _firemen.ToArray ();
	}

	public EntityBase[] GetGoblins() {
		return _goblins.ToArray ();
	}

	public EntityBase[] GetWatchmen() {
		return _watchmen.ToArray ();
	}

	public void DestroyEntity(EntityBase entity) {
		DestroyImmediate (entity);
	}
}
