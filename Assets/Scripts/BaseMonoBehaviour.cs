using UnityEngine;
using System.Collections;

public class BaseMonoBehaviour : MonoBehaviour {
	const float AITickInterval = 2;
	private static bool Paused;
	private bool _waiting;

	// Update is called once per frame
	void Update () {
		if (Paused) {
			return;
		}

		if (!_waiting) {
			_waiting = true;
			StartCoroutine (AIUpdateRoutine ());
		}
		//if ((_lastAITick - Time.realtimeSinceStartup) >= AITickInterval) {
		//	_lastAITick = Time.realtimeSinceStartup;
		//}

		GameUpdate ();
	}

	public virtual void GameUpdate() {

	}

	public virtual void AIUpdate() {

	}

	IEnumerator AIUpdateRoutine() {
		yield return new WaitForSeconds (AITickInterval);

		AIUpdate ();

		_waiting = false;
	}

	protected void Pause() {
		Paused = true;
	}

	protected void Unpause() {
		Paused = false;
	}
}
