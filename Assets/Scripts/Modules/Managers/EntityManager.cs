﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(GameModule))]
public class EntityManager : BaseMonoBehaviour {
	public GameObject GoblinPrefab;
	public GameObject WatchmanPrefab;
	public GameObject PicklePrefab;
	public GameObject FiremanPrefab;

	private readonly IList<EntityBase> _goblins = new List<EntityBase>();
	private readonly IList<EntityBase> _watchmen = new List<EntityBase>();
	private readonly IList<EntityBase> _pickles = new List<EntityBase>();
	private readonly IList<EntityBase> _firemen = new List<EntityBase>();

	public void MoveEntityTo(EntityBase entity, Vector2 newPosition) {

	}

	public EntityBase CreateGoblin(Vector2 position) {
		var goblin = Instantiate (GoblinPrefab);
	}

	public EntityBase CreateWatchman(Vector2 position) {
		return null;
	}

	public EntityBase CreatePickle(Vector2 position) {
		return null;
	}

	public EntityBase CreateFireman(Vector2 position) {
		return null;
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

	void DestroyEntity(EntityBase entity) {

	}
}
