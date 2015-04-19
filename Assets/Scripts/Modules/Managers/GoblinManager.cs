﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoblinManager : MonoBehaviour {
	
	public GameObject Goblinprefab;
	public float GoblinIgniteTilePercent = .5f;
	public EntityManager EntityManager;
	public PickleManager PickleManager;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
		// loop through every goblin
		foreach (GoblinEntity goblinEntity in EntityManager.GetGoblins()) {
			
			var tile = goblinEntity.GetTile();
			var location = goblinEntity.GetLocation();
			if (PickleManager.IsPicklePresent(location)){ // Are we at a pickle?
				// Eat it!
				PickleManager.EatPickleAtLocation(location);
			} else if (PickleManager.IsPickleInRange(location)){ // Is there a pickle in range?
				// Find it!
				EntityManager.MoveEntityTowards(goblinEntity, location);
			} else if (tile.CanSetOnFire()){ // Can we set something on fire?
				// Set it on fire!
				if (Random.Range(0F,1F) > GoblinIgniteTilePercent){
					tile.SetFire(FireStage.Kindling);
				}
			} else {
				// Move randomly looking for mischief
				var legalMoves = EntityManager.getLegalMoves(location);
				float countOfLegalMoves = (float) legalMoves.Count;
				int randomMove = Mathf.FloorToInt(Random.Range(0F, countOfLegalMoves));
				EntityManager.MoveEntityTo(goblinEntity, legalMoves[randomMove]);
			}
		}
	}
}