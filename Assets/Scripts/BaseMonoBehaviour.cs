using UnityEngine;
using System.Collections;

public class BaseMonoBehaviour : MonoBehaviour {
	const float AITickInterval = 2;
	private static bool Paused;
	private Coroutine AICoroutine;
	private int _ticks;

	// Update is called once per frame
	void Update () {
		if (Paused) {
			return;
		}

		_ticks++;

		if (AICoroutine == null) {
			AICoroutine = StartCoroutine (AIUpdateRoutine ());
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

		AICoroutine = null;
	}

	protected void Pause() {
		Paused = true;

		if (AICoroutine != null) {
			StopCoroutine(AICoroutine);
		}
	}

	protected void Unpause() {
		Paused = false;
	}

	/// <summary>
	/// The number of logic ticks that have gone by
	/// </summary>
	protected int GetTicks() {
		return _ticks;
	}
}
