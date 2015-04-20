using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(GameModule))]
public class WatchmanManager : BaseMonoBehaviour {

	public EntityManager EntityManager;
	public MapModule Map;
	public GameModule Game;
	public int WatchmanSightRange = 7;
	
	private Pathfinder _pathfinder;
	private EntityBase _chasingGoblin;
	private int _lastMovedOnTick;

	void Start() {
		_pathfinder = new Pathfinder (Map, EntityManager);
	}

	public void GenerateWatchmen() {

	}

	public void SpawnWatchman() {

	}

	// Update is called once per frame
	public override void AIUpdate () {

		// loop through every watchmen
		foreach (WatchmanEntity watchmanEntity in EntityManager.GetWatchmen()) {
			var tile = watchmanEntity.GetTile();
			var goblinLocation = Vector2.zero;

			if(tile.IsOnFire()) {
				// Die!
				// We're now dead.
			} else if(IsGoblinAdjacent(tile.Position, out goblinLocation)) {
				// Attack a goblin!
			} else if(_chasingGoblin != null) {
				// Move closer to this goblin.
				_lastMovedOnTick = GetTicks();
			} else if(IsGoblinNearby(tile.Position, WatchmanSightRange, out _chasingGoblin)) { // Look for a goblin

			} else { // Wander...
				_lastMovedOnTick = GetTicks();
			}
		}
	}

	bool IsGoblinNearby(Vector2 location, int distance, out EntityBase goblin) {
		goblin = EntityManager.GetClosestGoblin (location, distance);
		
		return goblin != null;
	}

	bool IsGoblinAdjacent(Vector2 location, out Vector2 goblinLocation) {
		var goblins = EntityManager.GetGoblins ();
		var checkableDirections = new Direction[] {
			Direction.Down,
			Direction.Up,
			Direction.Right,
			Direction.Left
		};

		foreach (var direction in checkableDirections.Select(d => d.ToVector2())) {
			var goblin = goblins.FirstOrDefault (g => g.GetTile ().Position == location + direction);

			if (goblin != null) {
				goblinLocation = goblin.GetTile().Position;
				return true;
			}
		}
		
		goblinLocation = Vector2.zero;
		return false;
	}
}