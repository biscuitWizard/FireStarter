using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GameModule))]
public class EntityManager : BaseMonoBehaviour {
	public GameObject GoblinPrefab;
	public GameObject WatchmanPrefab;
	public GameObject PicklePrefab;
	public GameObject FiremanPrefab;

	void MoveEntityTo(EntityBase entity, Vector2 newPosition) {

	}

	EntityBase CreateGoblin() {
		return null;
	}

	EntityBase CreateWatchman() {
		return null;
	}

	EntityBase CreatePickle() {
		return null;
	}

	EntityBase CreateFireman() {
		return null;
	}

	void DestroyEntity(EntityBase entity) {

	}
}
