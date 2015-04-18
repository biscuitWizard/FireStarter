using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoblinManager : MonoBehaviour {
	
	public GameObject Goblinprefab;
	public float GoblinIgniteTilePercent = .5f;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
		// loop through every goblin
		foreach (GoblinEntity goblinEntity in EntityManager.GetGoblins()) {
			
			var tile = goblinEntity.GetTile();
			var location = goblinEntity.GetLocation();
			if (PickleManager.isPicklePresent(location)){ // Are we at a pickle?
				// Eat it!
				
			} else if (PickleManager.isPickleInRange(location)){ // Is there a pickle in range?
				// Find it!
				
			} else if (tile.CanSetOnFire()){ // Can we set something on fire?
				// Set it on fire!
				if (Random.Range(0F,1F) > GoblinIgniteTilePercent){
					tile.SetFire(FireStage.Kindling);
				}
			} else {
				// Move randomly looking for mischief
			}
		}
	}
}