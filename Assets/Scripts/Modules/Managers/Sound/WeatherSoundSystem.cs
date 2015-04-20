using UnityEngine;
using System.Collections;

public class WeatherSoundSystem : SoundMonoBehaviour {

	// Use this for initialization
	void Start () {
		Messenger.AddListener ("stopRain", StopRain);
		Messenger.AddListener ("startRain", StartRain);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void StartRain() {

	}

	void StopRain() {

	}
}
