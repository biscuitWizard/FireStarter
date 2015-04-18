using UnityEngine;
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

	void DestroyEntity(EntityBase entity) {

	}
}
