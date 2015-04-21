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
		EntityManager.CreateWatchman (new Vector2 (0, Map.MapSize.y - 1));
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
				EntityManager.DestroyWatcham(watchmanEntity);
			} else if(IsGoblinAdjacent(tile.Position, out goblinLocation)) {
				// Attack a goblin!
				Messenger.Broadcast ("playBillyBeat");
				EntityManager.DestroyGoblin(EntityManager.GetGoblins().First (g => g.GetTile ().Position == goblinLocation));
			} else if(_chasingGoblin != null) {
				// Move closer to this goblin.
				_lastMovedOnTick = GetTicks();
				var path = _pathfinder.Navigate(tile.Position, _chasingGoblin.GetTile().Position);
				if(path.Length < 2) {
					_chasingGoblin = null;
				} else {
					EntityManager.MoveEntityTo(watchmanEntity, path[0]);
				}
			} else if(IsGoblinNearby(tile.Position, WatchmanSightRange, out _chasingGoblin)) { // Look for a goblin

			} else { // Wander...
				_lastMovedOnTick = GetTicks();
				var legalMoves = EntityManager.getLegalMoves(tile.Position);
				EntityManager.MoveEntityTo(watchmanEntity,
				                           legalMoves.OrderBy (x => System.Guid.NewGuid()).First ());
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