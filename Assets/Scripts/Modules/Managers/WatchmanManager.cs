using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(GameModule))]
public class WatchmanManager : BaseMonoBehaviour {

	public EntityManager EntityManager;
	public GoblinManager GoblinManager;
	public GameModule Game;

	private float _lastSpawnTime = 0;
	private int _watchmenSpawnSecondsInterval = 3;

	// Update is called once per frame
	public override void GameUpdate () {

		// loop through every watchmen
		foreach (WatchmanEntity watchmanEntity in EntityManager.GetWatchmen()) {

			GoblinManager.GoblinDistance closestGoblinDistance = GoblinManager.GetClosestGoblinDistanceInRange(watchmanEntity.GetLocation());
			if (Random.Range(0F, 6F) > closestGoblinDistance.Distance){ // guard passes perpection, move towards goblins

				EntityManager.MoveEntityTowards(watchmanEntity, closestGoblinDistance.Goblin.GetLocation(), 2);
			} else { // guard is drunk, move randomly

				var legalMoves = EntityManager.getLegalMoves(watchmanEntity.GetLocation());
				float countOfLegalMoves = (float) legalMoves.Count;
				int randomMove = Mathf.FloorToInt(Random.Range(0F, countOfLegalMoves));
				EntityManager.MoveEntityTo(watchmanEntity, legalMoves[randomMove]);
			}
		}

		// spawn new watchmen
		SpawnWatchmen ();
	}

	void SpawnWatchmen () {

		if ((_lastSpawnTime - Time.realtimeSinceStartup) >= _watchmenSpawnSecondsInterval) {
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
				spawnLocation = new Vector2(Random.Range(0, mapSize.y), 0);
				break;
			case 4:
				spawnLocation = new Vector2(Random.Range(0, mapSize.x), mapSize.y-1);
				break;
			}
			EntityManager.CreateWatchman(spawnLocation);
			_lastSpawnTime = Time.realtimeSinceStartup;
		}
	}
}