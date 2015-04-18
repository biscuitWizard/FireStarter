using UnityEngine;
using System.Collections;

public enum FireStage {
	Kindling,
	Hazard,
	Raging
}

public class FireAutomata : BaseMonoBehaviour {

	public bool Contagious = true;
	public FireStage Stage = FireStage.Kindling;
	public float NearbyTileFlammabilityPercent = .2f;
	public float SecondsBetweenTicks = 2;
	public Vector2 TilePosition = new Vector2(0, 0);
	public Vector2[] FlammableDirections = new Vector2[] {
		Vector2.up,
		Vector2.up * -1,
		Vector2.right,
		Vector2.right * -1,
		Vector2.up + Vector2.right,
		(Vector2.up * -1) + Vector2.right,
		Vector2.up + (Vector2.right * -1),
		(Vector2.up * -1) + (Vector2.right * -1)
	};

	private bool _waiting = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!_waiting
		    	&& Contagious) {
			_waiting = true;
			StartCoroutine(IgniteNeighborsAndWait());
		}
	}

	IEnumerator IgniteNeighborsAndWait() {
		foreach (var direction in FlammableDirections) {
			if(Random.Range (0f, 1f) < NearbyTileFlammabilityPercent) {
				Debug.Log (string.Format ("New tile has caught on fire @ {0}", TilePosition + direction));
			}
		}

		yield return new WaitForSeconds(SecondsBetweenTicks);
		_waiting = false;
	}
}
