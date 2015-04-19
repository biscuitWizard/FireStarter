using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FiremanManager : BaseMonoBehaviour {

	public EntityManager EntityManager;
	
	private float _lastSpawnTime = 0;
	private int _firemenSpawnSecondsInterval = 3;
	
	// TODO: change this to the correct value
	private int _mapMax = 10;

	// Use this for initialization
	void Start () {
	}
	
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
			
			// TODO: is map 0-based or 1-based?
			// Determine board edge
			int boardEdge = Random.Range(1,5);
			Vector2 spawnLocation;
			switch (boardEdge) {
			case 1:
				spawnLocation = new Vector2(1, Random.Range(1, _mapMax+1));
				break;
			case 2:
				spawnLocation = new Vector2(_mapMax, Random.Range(1, _mapMax+1));
				break;
			case 3:
				spawnLocation = new Vector2(Random.Range(1, _mapMax+1), 1);
				break;
			case 4:
				spawnLocation = new Vector2(Random.Range(1, _mapMax+1), _mapMax);
				break;
			}
			EntityManager.CreateWatchman(spawnLocation);
			_lastSpawnTime = Time.realtimeSinceStartup;
		}
	}
}