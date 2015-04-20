using UnityEngine;
using System.Collections;

public class GoblinSoundManager : SoundMonoBehaviour {

	// Use this for initialization
	void Start () {
		Messenger.AddListener ("playRandomGoblinLyric", OnPlayRandomGoblinLyric);
		Messenger.AddListener ("playGoblinDeath", OnPlayGoblinDeath);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnPlayRandomGoblinLyric() {

	}

	void OnPlayGoblinDeath() {

	}
}
