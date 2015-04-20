using UnityEngine;
using System.Collections;
using FMOD.Studio;

[RequireComponent(typeof(FMOD_Listener))]
public class SympathyListener : MonoBehaviour 
{
	private static SympathyListener __chosenOne = null;
	
	void Awake()
	{
		// There can only be one !
		if(__chosenOne == null)
		{
			Debug.Log ("SympathyFMOD_Listener " + this.GetInstanceID() + " is the singleton");
			// *I* am the milkman (my milk is delicious)
			__chosenOne = this;
			
			// Only ever have one FMOD_Listener active at a time!
			GetComponent<FMOD_Listener>().enabled = true;
			
			// This one should remain
			DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			Debug.Log ("SympathyFMOD_Listener " + this.GetInstanceID() + "  will self-destruct");
			// Seppuku!
			Destroy(this.gameObject);
		}
	}
	
	void Start()
	{
		if(__chosenOne == this)
		{
			// FMOD studio system should not be destroyed either
			DontDestroyOnLoad(GameObject.Find("FMOD_StudioSystem"));
		}
	}
	
	void Update ()
	{
		transform.position = Camera.main.transform.position;
	}
}