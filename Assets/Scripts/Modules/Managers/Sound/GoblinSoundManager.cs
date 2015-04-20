using UnityEngine;
using System.Collections;
using FMOD.Studio;

public class GoblinSoundManager : SoundMonoBehaviour {

	public FMODAsset GoblinLaugh;
	public FMODAsset GoblinLyrics;
	public FMODAsset GoblinDeath;

	// Use this for initialization
	void Start () {
		Messenger.AddListener ("playRandomGoblinLyric", OnPlayRandomGoblinLyric);
		Messenger.AddListener ("playGoblinDeath", OnPlayGoblinDeath);
		Messenger.AddListener ("playRandomGoblinLaugh", OnPlayRandomGoblinLaugh);
	}

	void OnPlayRandomGoblinLyric() {
		StudioSystem.PlayOneShot (GoblinLyrics, Camera.main.transform.position);
	}

	void OnPlayGoblinDeath() {
		StudioSystem.PlayOneShot (GoblinDeath, Camera.main.transform.position);
	}

	void OnPlayRandomGoblinLaugh() {
		StudioSystem.PlayOneShot (GoblinLaugh, Camera.main.transform.position);
	}
}
