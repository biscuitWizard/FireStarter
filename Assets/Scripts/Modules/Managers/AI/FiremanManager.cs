using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(GameModule))]
public class FiremanManager : BaseMonoBehaviour {

	public EntityManager EntityManager;
	public MapModule Map;
	public FireManager FireManager;
	public GameModule Game;
	public int FiremanSightRange = 10;
	
	private float _lastSpawnTime = 0;
	private int _firemenSpawnSecondsInterval = 3;
	private Pathfinder _pathfinder;
	private List<Vector2> _currentPath;

	void Start() {
		_pathfinder = new Pathfinder (Map, EntityManager);
	}

	// Update is called once per frame
	public override void AIUpdate () {

		// loop through every watchmen
		foreach (FiremanEntity firemanEntity in EntityManager.GetFiremen()) {

			var tile = firemanEntity.GetTile();
			var fire = Vector2.zero;

			// Move towards fire.
			if(_currentPath != null) {
				EntityManager.MoveEntityTo(firemanEntity, _currentPath[0]);
				_currentPath.RemoveAt (0);

				// We don't want to move on to fire tiles. Just next to them.
				if(_currentPath.Count == 1) {
					_currentPath = null;
				}
			} else if(IsFireAdjacent(tile.Position, out fire)) { // Extinguish an adjacent fire.
				// Get the tile.
				var fireTile = Map.GetTile (fire);

				// Extinguish the fire.
				FireManager.ExtinguishFire(fireTile.Position);
			} else if(IsFireNearby(tile.Position, FiremanSightRange, out fire)) { // Find the nearest fire.
				// Navigate to path.
				_currentPath = _pathfinder.Navigate(tile.Position, fire);

				// Move along that path by one.
				AIUpdate();
			} else { // wander
				var legalMoves = EntityManager.getLegalMoves(tile.Position);
				EntityManager.MoveEntityTo(firemanEntity, legalMoves.OrderBy (x => System.Guid.NewGuid()).First ());
			}
		}
	}

	void SpawnFiremen () {
		
		if ((_lastSpawnTime - Time.realtimeSinceStartup) >= _firemenSpawnSecondsInterval) {
			var mapSize = Game.GetMapSize();

			// Determine board edge
			int boardEdge = Random.Range(1,5);
			Vector2 spawnLocation = Vector2.zero;
			switch (boardEdge) {
			case 1:
				spawnLocation = new Vector2(0, Random.Range(0, mapSize.y));
				break;
			case 2:
				spawnLocation = new Vector2(mapSize.x-1, Random.Range(0, mapSize.y));
				break;
			case 3:
				spawnLocation = new Vector2(Random.Range(0, mapSize.x), 0);
				break;
			case 4:
				spawnLocation = new Vector2(Random.Range(0, mapSize.x), mapSize.y-1);
				break;
			}
			EntityManager.CreateWatchman(spawnLocation);
			_lastSpawnTime = Time.realtimeSinceStartup;
		}
	}

	public void GenerateFiremen() {
		EntityManager.CreateFireman(Vector2.zero);
	}

	bool IsFireAdjacent(Vector2 location, out Vector2 fire) {
		var fires = FireManager.GetBurningTiles ();
		var checkableDirections = new Direction[] {
			Direction.Up,
			Direction.Down,
			Direction.Left,
			Direction.Right
		};

		foreach (var direction in checkableDirections.Select(d => d.ToVector2())) {
			var burningTile = fires.FirstOrDefault (f => f.Position == location + direction);
			
			if (burningTile != null) {
				fire = burningTile;
				return true;
			}
		}

		return false;
	}

	bool IsFireNearby(Vector2 location, int distance, out Vector2 fire) {
		fire = FireManager.GetClosestBurningTile (location, distance);

		return fire != null;
	}
}