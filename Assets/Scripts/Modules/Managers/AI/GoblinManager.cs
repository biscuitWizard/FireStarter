using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoblinManager : BaseMonoBehaviour {

	public EntityManager EntityManager;
	public PickleManager PickleManager;
	public GameModule Game;
	
	public float GoblinIgniteTilePercent = .5f;

	// Update is called once per frame
	public override void AIUpdate () {

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
				Debug.Log(string.Format("Going to try to move to ({0},{1}).", legalMoves[randomMove].x, legalMoves[randomMove].y));
				EntityManager.MoveEntityTo(goblinEntity, legalMoves[randomMove]);
			}
		}
	}

	public void GenerateGoblins() {
		var mapSize = Game.GetMapSize();
		var x = Random.Range (0, mapSize.x - 1);
		var y = Random.Range (0, mapSize.y - 1);
		EntityManager.CreateGoblin (new Vector2 (x, y));
	}
}