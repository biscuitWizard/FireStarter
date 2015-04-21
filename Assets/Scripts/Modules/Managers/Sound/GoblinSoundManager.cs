using UnityEngine;
using System.Collections;
using FMOD.Studio;

public class GoblinSoundManager : SoundMonoBehaviour {

	public FMODAsset GoblinLaugh;
	public FMODAsset GoblinLyrics;
	public FMODAsset GoblinDeath;

	private EventInstance _goblinLaugh;
	private EventInstance _goblinLyrics;
	private EventInstance _goblinDeath;

	// Use this for initialization
	void Awake () {
		Messenger.AddListener ("playRandomGoblinLyric", OnPlayRandomGoblinLyric);
		Messenger.AddListener ("playGoblinDeath", OnPlayGoblinDeath);
		Messenger.AddListener ("playRandomGoblinLaugh", OnPlayRandomGoblinLaugh);
	}

	void Start() {
		_goblinLaugh = StudioSystem.GetEvent (GoblinLaugh.path);
		_goblinLyrics = StudioSystem.GetEvent (GoblinLyrics.path);
		_goblinDeath = StudioSystem.GetEvent (GoblinDeath.path);
	}

	void OnPlayRandomGoblinLyric() {
		if (IsSoundPlaying (_goblinLyrics)) {
			return;
		}

		_goblinLyrics.start ();
	}

	void OnPlayGoblinDeath() {
		if (IsSoundPlaying (_goblinDeath)) {
			return;
		}
		
		_goblinDeath.start ();
	}

	void OnPlayRandomGoblinLaugh() {
		if (IsSoundPlaying (_goblinLaugh)) {
			return;
		}
		
		_goblinLaugh.start ();
	}

	void OnDestroy() {
		_goblinLaugh.stop (STOP_MODE.IMMEDIATE);
		_goblinLaugh.release ();
		_goblinDeath.stop (STOP_MODE.IMMEDIATE);
		_goblinDeath.release ();
		_goblinLyrics.stop (STOP_MODE.IMMEDIATE);
		_goblinLyrics.release ();
	}
}
