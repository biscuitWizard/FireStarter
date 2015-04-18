using UnityEngine;
using System.Collections;

public class BaseMonoBehaviour : MonoBehaviour {
	private static bool Paused;
	
	// Update is called once per frame
	void Update () {
		if (Paused) {
			return;
		}

		GameUpdate ();
	}

	public virtual void GameUpdate() {

	}

	protected void Pause() {
		Paused = true;
	}

	protected void Unpause() {
		Paused = false;
	}
}
