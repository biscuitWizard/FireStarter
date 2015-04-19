using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(GameModule))]
public class FiremanManager : BaseMonoBehaviour {

	public EntityManager EntityManager;
	public GameModule Game;
	
	private float _lastSpawnTime = 0;
	private int _firemenSpawnSecondsInterval = 3;

	// Update is called once per frame
	public override void GameUpdate () {

		// loop through every watchmen
		foreach (FiremanEntity firemanEntity in EntityManager.GetFiremen()) {

			var tile = firemanEntity.GetTile();
			if (tile.IsOnFire()){ // are we at a fire?

				// put out the fire
				tile.SetFire(FireStage.Flammable);
			} else { // find a fire

				// TODO: make firemen move towards fire
				var legalMoves = EntityManager.getLegalMoves(firemanEntity.GetLocation());
				float countOfLegalMoves = (float) legalMoves.Count;
				int randomMove = Mathf.FloorToInt(Random.Range(0F, countOfLegalMoves));
				EntityManager.MoveEntityTo(firemanEntity, legalMoves[randomMove]);
			}
		}

		// spawn new watchmen
		SpawnFiremen ();
	}

	void SpawnFiremen () {
		
		if ((_lastSpawnTime - Time.realtimeSinceStartup) >= _firemenSpawnSecondsInterval) {
			var mapSize = Game.GetMapSize();

			// TODO: is map 0-based or 1-based?
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
}