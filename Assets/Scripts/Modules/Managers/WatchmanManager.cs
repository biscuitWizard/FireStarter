using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WatchmanManager : BaseMonoBehaviour {

	public EntityManager EntityManager;

	private float _lastSpawnTime = 0;
	private int _watchmenSpawnSecondsInterval = 3;

	// TODO: change this to the correct value
	private int _mapMax = 10;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	public override void GameUpdate () {

		// loop through every watchmen
		foreach (WatchmanEntity watchmanEntity in EntityManager.GetWatchmen()) {

			// TODO: make watchmen move towards goblins
			var legalMoves = EntityManager.getLegalMoves(watchmanEntity.GetLocation());
			float countOfLegalMoves = (float) legalMoves.Count;
			int randomMove = Mathf.FloorToInt(Random.Range(0F, countOfLegalMoves));
			EntityManager.MoveEntityTo(watchmanEntity, legalMoves[randomMove]);
		}

		// spawn new watchmen
		SpawnWatchmen ();
	}

	void SpawnWatchmen () {

		if ((_lastSpawnTime - Time.realtimeSinceStartup) >= _watchmenSpawnSecondsInterval) {
			
			// TODO: is map 0-based or 1-based?
			// Determine board edge
			int boardEdge = Random.Range(1,5);
			Vector2 spawnLocation = Vector2.zero;
			switch (boardEdge) {
			case 1:
				spawnLocation = new Vector2(0, Random.Range(0, _mapMax));
				break;
			case 2:
				spawnLocation = new Vector2(_mapMax-1, Random.Range(0, _mapMax));
				break;
			case 3:
				spawnLocation = new Vector2(Random.Range(0, _mapMax), 0);
				break;
			case 4:
				spawnLocation = new Vector2(Random.Range(0, _mapMax), _mapMax-1);
				break;
			}
			EntityManager.CreateWatchman(spawnLocation);
			_lastSpawnTime = Time.realtimeSinceStartup;
		}
	}
}