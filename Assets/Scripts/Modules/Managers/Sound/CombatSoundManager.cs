using UnityEngine;
using System.Collections;
using FMOD.Studio;

public class CombatSoundManager : SoundMonoBehaviour {

	public FMODAsset BillyClub;

	private EventInstance _billyClub;

	// Use this for initialization
	void Awake () {
		Messenger.AddListener ("playBillyBeat", OnPlayBillyBeat);
	}

	void Start () {
		_billyClub = StudioSystem.GetEvent (BillyClub.path);
	}

	void OnPlayBillyBeat(){
		if (IsSoundPlaying (_billyClub)) {
			return;
		}
	}

	void OnDestroy() {
		_billyClub.stop (STOP_MODE.IMMEDIATE);
		_billyClub.release ();
	}
}
