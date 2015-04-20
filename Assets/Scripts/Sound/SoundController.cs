using UnityEngine;
using System.Collections;
using FMOD.Studio;

[RequireComponent(typeof(FMOD_StudioSystem))]
public class SoundController : MonoBehaviour {

	public float MasterVolume = 1.0f;

	private Bus _masterBus;

	// Use this for initialization
	void Start () {
		FMOD_StudioSystem.instance.System.getBus("bus:/SFX", out _masterBus);
	}

	void Update()
	{
		//_masterBus.setFaderLevel (MasterVolume);
	}
}
