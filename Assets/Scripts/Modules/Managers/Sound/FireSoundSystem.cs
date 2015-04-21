using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FMOD.Studio;

public class FireSoundSystem : SoundMonoBehaviour {

	public float MasterVolume = 1;

	public FMODAsset FireLoop;
	public FMODAsset FireStart;
	public FMODAsset BuildingDestruction;
	public FMODAsset FireExtinguished;

	private EventInstance _fireLoop;
	private EventInstance _fireStart;
	private EventInstance _buildingDestruction;
	private EventInstance _fireExtinguished;
	private float _lastFireLoopIntensityAverage = 0;
	private readonly Dictionary<Vector2, float> _fireLoopIntensities = new Dictionary<Vector2, float>();

	// Use this for initialization
	void Awake () {
		Messenger<Vector2, float>.AddListener ("playFireLoop", OnPlayFireLoop);
		Messenger<Vector2>.AddListener ("stopFireLoop", OnStopFireLoop);
		Messenger<Vector2, float>.AddListener ("setFireLoopIntensity", OnSetFireLoopIntensity);
		Messenger.AddListener ("playFireStart", OnPlayFireStart);
		Messenger.AddListener ("playBuildingDestruction", OnPlayBuildingDestruction);
		Messenger.AddListener ("playFireExtinguished", OnPlayFireExtinguished);
	}

	void Start() {
		_fireLoop = StudioSystem.GetEvent (FireLoop.path);
		_fireStart = StudioSystem.GetEvent (FireStart.path);
		_fireExtinguished = StudioSystem.GetEvent (FireExtinguished.path);
		_buildingDestruction = StudioSystem.GetEvent (BuildingDestruction.path);
	}
	
	// Update is called once per frame
	void Update () {
		if (_fireLoopIntensities.Count == 0)
			return;

		// Get average of all intensities and set.
		var intensityAverage = _fireLoopIntensities.Values.Sum () / _fireLoopIntensities.Count;
		if(intensityAverage != _lastFireLoopIntensityAverage) {
			_fireLoop.setParameterValue ("intensity", intensityAverage);

			_lastFireLoopIntensityAverage = intensityAverage;
		}
	}

	void OnPlayFireLoop(Vector2 tile, float intensity) {
		if (_fireLoop == null)
			return;

		if (!_fireLoopIntensities.ContainsKey (tile)) {
			_fireLoopIntensities.Add (tile, intensity);
		}

		if (IsSoundPlaying (_fireLoop)) {
			return;
		}
		_fireLoop.start ();
		_fireLoop.setVolume (MasterVolume);
	}

	void OnStopFireLoop(Vector2 tile) {
		_fireLoopIntensities.Remove (tile);

		if (_fireLoopIntensities.Count == 0) {
			_fireLoop.stop (STOP_MODE.ALLOWFADEOUT);
		}
	}

	void OnPlayFireStart() {
		if (IsSoundPlaying (_fireStart)) {
			return;
		}

		if (_fireStart == null)
			return;

		_fireStart.start ();
		_fireStart.setVolume (MasterVolume);
	}

	void OnPlayBuildingDestruction() {
		if (IsSoundPlaying (_buildingDestruction)) {
			return;
		}

		_buildingDestruction.start ();
		//_buildingDestruction.setVolume (MasterVolume);
	}

	void OnSetFireLoopIntensity(Vector2 tile, float intensity) {
		if (!_fireLoopIntensities.ContainsKey (tile)) {
			Debug.LogError (string.Format ("Tile {0} has incorrectly attempted to set an intensity for a non-playing fireloop.", tile));
			return;
		}

		_fireLoopIntensities [tile] = intensity;
	}

	void OnPlayFireExtinguished() {
		_fireExtinguished.start ();
		_fireExtinguished.setVolume (MasterVolume);

		StartCoroutine (StopExtinguishingFire ());
	}

	IEnumerator StopExtinguishingFire() {
		yield return new WaitForSeconds (1.5f);
		_fireExtinguished.stop (STOP_MODE.ALLOWFADEOUT);
	}

	void OnDestroy() {
		Debug.Log ("Cleaning up sound...");
		_fireLoop.stop (STOP_MODE.IMMEDIATE);
		_fireLoop.release ();
		_fireStart.stop (STOP_MODE.IMMEDIATE);
		_fireStart.release ();
		_fireExtinguished.stop (STOP_MODE.IMMEDIATE);
		_fireExtinguished.release ();
		_buildingDestruction.stop (STOP_MODE.IMMEDIATE);
		_buildingDestruction.release ();
	}
}
