using UnityEngine;
using System.Collections;
using System.Linq;
using FMOD.Studio;

[RequireComponent(typeof(TileRenderer))]
public class FlammableTile : BaseMonoBehaviour {
	public Sprite ScorcedTileSprite;
	public FirePrefab[] FirePrefabs;

	public float MinStageLifetime = 8f;
	public float MaxStageLifetime = 14f;

	[ReadOnly] public float RemainingStageTime = 0; // This is for debug only.

	private FireStage _currentStage;
	private float[] _stageLifetimes;
	private TileRenderer _tileRenderer;
	private float _lastStageChange;

	void Awake() {
		_stageLifetimes = new float[] {
			Random.Range (MinStageLifetime, MaxStageLifetime),
			Random.Range (MinStageLifetime, MaxStageLifetime),
			Random.Range (MinStageLifetime, MaxStageLifetime)
		};

		_tileRenderer = GetComponent<TileRenderer>();
	}

	public override void GameUpdate() {
		// Not on fire
		if (_currentStage == FireStage.NotFlammable
			|| _currentStage == FireStage.Flammable
		    || _currentStage == FireStage.BurntOut) {
			return;
		}

		// Debug Only
		RemainingStageTime = (_lastStageChange + _stageLifetimes [(int)_currentStage - 1]) - Time.realtimeSinceStartup;

		if(Time.realtimeSinceStartup - _lastStageChange >= _stageLifetimes[(int)_currentStage - 1]) {

			// On next stage
			_currentStage = (FireStage)((int)_currentStage + 1);

			// Update sound intensity.
			Messenger<Vector2, float>.Broadcast ("setFireLoopIntensity", _tileRenderer.Tile.Position,
			                                     ((int)_currentStage - 1) * 3);

			if((_currentStage == FireStage.BurntOut)) {
				// We're done!
				_tileRenderer.ClearFire();
				_tileRenderer.Tile.spriteRenderer.sprite = ScorcedTileSprite;
				
				// Stop the sound.
				Messenger<Vector2>.Broadcast ("stopFireLoop", _tileRenderer.Tile.Position);
				Messenger.Broadcast ("playBuildingDestruction");
			} else { // Render next fire stage.
				_tileRenderer.RenderFire (FirePrefabs.First (p => p.Stage == _currentStage).Prefab);
			}

			_lastStageChange = Time.realtimeSinceStartup;
		}
	}

	public void StartFire(FireStage stage) {
		_currentStage = stage;
		_lastStageChange = Time.realtimeSinceStartup;
		if (stage == FireStage.BurntOut
			|| stage == FireStage.Flammable
			|| stage == FireStage.NotFlammable) {
			_tileRenderer.ClearFire ();
		} else {
			if(stage == FireStage.Kindling) {
				// Start the sounds.
				Messenger<Vector2, float>.Broadcast ("playFireLoop", _tileRenderer.Tile.Position, 0f);
				Messenger.Broadcast ("playFireStart");
			}
			_tileRenderer.RenderFire (FirePrefabs.First (p => p.Stage == _currentStage).Prefab);
		}
	}

	public void StopFire() {
		_currentStage = _tileRenderer.Tile.StartingFireStage;
		Messenger<Vector2>.Broadcast ("stopFireLoop", _tileRenderer.Tile.Position);
	}

	public FireStage GetFireStage() {
		return _currentStage;
	}
}

[System.Serializable]
public class FirePrefab {
	public FireStage Stage;
	public GameObject Prefab;
}
