using UnityEngine;
using System.Collections;
using FMOD.Studio;

public class SoundMonoBehaviour : MonoBehaviour {

	public FMOD_StudioSystem StudioSystem {
		get {
			return FMOD_StudioSystem.instance;
		}
	}

	protected bool IsSoundPlaying(EventInstance instance) {
		var playbackState = PLAYBACK_STATE.STOPPED;

		instance.getPlaybackState (out playbackState);

		return playbackState == PLAYBACK_STATE.PLAYING;
	}
}
