using UnityEngine;
using System.Collections;
using FMOD.Studio;

public class SoundMonoBehaviour : MonoBehaviour {

	public FMOD_StudioSystem StudioSystem {
		get {
			return FMOD_StudioSystem.instance;
			//return GetComponentInParent<FMOD_StudioSystem>();
		}
	}

	protected bool IsSoundPlaying(EventInstance instance) {
		if (instance == null) {
			Debug.LogError ("Null event instance");
			return false;
		}

		var playbackState = PLAYBACK_STATE.STOPPED;

		instance.getPlaybackState (out playbackState);

		return playbackState == PLAYBACK_STATE.PLAYING;
	}

}
