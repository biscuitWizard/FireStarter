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

	public int PickleSightRange = 10;
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
			var pickleLocation = Vector2.zero;

			// If we are currently on a path, keep traversing that path.
			if(_currentPath != null) {
				// Check to make sure the pickle is still there..
				if(!PickleManager.IsPicklePresent(_currentPath.Last ())) {
					_currentPath = null;
				} else {
					EntityManager.MoveEntityTo(goblinEntity, _currentPath[0]);
					_currentPath.RemoveAt(0);

					// If the path is empty, nuke it.
					if(_currentPath.Count == 0) {
						_currentPath = null;
					}
				}
			} else if (PickleManager.IsPicklePresent(location)){ // Are we at a pickle?
				// Eat it!
				PickleManager.EatPickleAtLocation(location);
			} else if (IsPickleNearby(location, PickleSightRange, out pickleLocation)){ // Is there a pickle in range?
				// Find it!
				_currentPath = new List<Vector2>();
				_currentPath.AddRange (_pathfinder.Navigate(tile.Position, pickleLocation));

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
				EntityManager.MoveEntityTo(goblinEntity, legalMoves[randomMove]);
			}
		}
	}

	public void GenerateGoblins() {
		var mapSize = Game.GetMapSize ();
		for (int i=0; i < 2; i++) {
			var x = Random.Range (0, mapSize.x - 1);
			var y = Random.Range (0, mapSize.y - 1);
			EntityManager.CreateGoblin (new Vector2 (x, y));
		}
	}

	public bool IsPickleNearby(Vector2 location, int distance, out Vector2 target) {
		var closestPickle = PickleManager.GetClosestPickleInRange (location, distance);

		if (closestPickle != null) {
			target = closestPickle.GetTile ().Position;
			return true;
		}

		target = Vector2.zero;

		return false;

	}

	public void KillGoblin(GoblinEntity goblin) {
		EntityManager.DestroyGoblin (goblin);
		Messenger.Broadcast ("playGoblinDeath");
	}
}