using UnityEngine;
using System.Collections;
using FMOD.Studio;

public class GoblinSoundManager : SoundMonoBehaviour {

	public FMODAsset GoblinLaugh;
	public FMODAsset GoblinLyrics;
	public FMODAsset GoblinDeath;

	EventInstance _goblinLaugh;
	EventInstance _goblinLyrics;
	EventInstance _goblinDeath;

	// Use this for initialization
	void Start () {
		Messenger.AddListener ("playRandomGoblinLyric", OnPlayRandomGoblinLyric);
		Messenger.AddListener ("playGoblinDeath", OnPlayGoblinDeath);
		Messenger.AddListener ("playRandomGoblinLaugh", OnPlayRandomGoblinLaugh);
		_goblinLaugh = StudioSystem.GetEvent (GoblinLaugh.path);
		_goblinDeath = StudioSystem.GetEvent (GoblinDeath.path);
		_goblinLyrics = StudioSystem.GetEvent (GoblinLyrics.path);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnPlayRandomGoblinLyric() {
		if (IsSoundPlaying (_goblinLyrics)) {
			return;
		}

		_goblinLyrics.start ();
	}

	void OnPlayGoblinDeath() {
		_goblinDeath.start ();
	}

	void OnPlayRandomGoblinLaugh() {
		if (IsSoundPlaying (_goblinLaugh)) {
			return;
		}
		
		_goblinLaugh.start ();
	}
}
