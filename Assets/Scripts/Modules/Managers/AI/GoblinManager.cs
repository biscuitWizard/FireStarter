using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(EntityManager), typeof(FireManager))]
public class GoblinManager : BaseMonoBehaviour {

	public EntityManager EntityManager;
	public PickleManager PickleManager;
	public FireManager FireManager;
	public MapModule Map;
	public GameModule Game;
	
	public float GoblinIgniteTilePercent = .5f;
	public float GoblinSpeechPercent = 0.60f;
	public float GoblinLaughPercentage = 0.75f;

	private List<Vector2> _currentPath;
	private Pathfinder _pathfinder;

	void Awake() {
		_pathfinder = new Pathfinder (Map, EntityManager);
	}

	// Update is called once per frame
	public override void AIUpdate () {

		// loop through every goblin
		foreach (GoblinEntity goblinEntity in EntityManager.GetGoblins()) {
			
			var tile = goblinEntity.GetTile();
			var location = goblinEntity.GetLocation();
			// If we are currently on a path, keep traversing that path.
			if(_currentPath != null) {
				EntityManager.MoveEntityTo(goblinEntity, _currentPath[0]);
				_currentPath.RemoveAt(0);

				// If the path is empty, nuke it.
				if(_currentPath.Count == 0) {
					_currentPath = null;
				}
			} else if (PickleManager.IsPicklePresent(location)){ // Are we at a pickle?
				// Eat it!
				PickleManager.EatPickleAtLocation(location);
			} else if (PickleManager.IsPickleInRange(location)){ // Is there a pickle in range?
				// Find it!
				_currentPath.AddRange (_pathfinder.Navigate(tile.Position, location));
				AIUpdate();

			} else if (tile.CanSetOnFire()){ // Can we set something on fire?
				// Set it on fire!
				if (Random.Range(0F,1F) < GoblinIgniteTilePercent){
					if(Random.Range (0f, 1f) < GoblinSpeechPercent) {
						if(Random.Range (0f, 1f) < GoblinLaughPercentage) {
							Messenger.Broadcast ("playRandomGoblinLaugh");
						} else {
							Messenger.Broadcast ("playRandomGoblinLyric");
						}
					}

					FireManager.StartFire(tile.Position, FireStage.Kindling);
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
		var mapSize = Game.GetMapSize ();
		var x = Random.Range (0, mapSize.x - 1);
		var y = Random.Range (0, mapSize.y - 1);
		EntityManager.CreateGoblin (new Vector2 (x, y));
	}

	public GoblinDistance GetClosestGoblinDistanceInRange(Vector2 location, int distance = 5) {
		var goblinsInRange = EntityManager.GetGoblins ().Select (g => {
			var goblinLocation = g.GetLocation();
			var xd = goblinLocation.x - location.x;
			var yd = goblinLocation.y - location.y;
			
			return new GoblinDistance(g, Mathf.Sqrt(Mathf.Pow (xd, 2) + Mathf.Pow (yd,2)));
		})
			.Where (gd => gd.Distance <= distance)
				.OrderBy (gd => gd.Distance);
		
		return goblinsInRange.First();
	}
	
	public class GoblinDistance {
		public EntityBase Goblin;
		public float Distance;
		
		public GoblinDistance(EntityBase goblin, float distance) {
			Goblin = goblin;
			Distance = distance;
		}
	}
}