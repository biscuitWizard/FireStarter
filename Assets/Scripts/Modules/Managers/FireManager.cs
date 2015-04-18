using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireManager : BaseMonoBehaviour {

	public GameObject StarterFirePrefab;
	public float NearbyTileFlammabilityPercent = .2f;
	public float FireSpreadSecondsInterval = 3f;
	public int StartingFires = 2;
	private Vector2 _mapSize;
	private float _lastCheck;
	private readonly List<Fire> _activeFires = new List<Fire>();

	// Use this for initialization
	void Awake () {
		Messenger<Vector2>.AddListener ("mapInitialized", OnMapInitialized);
	}
	
	// Update is called once per frame
	public override void GameUpdate () {
		if ((_lastCheck - Time.realtimeSinceStartup) >= FireSpreadSecondsInterval) {


			_lastCheck = Time.realtimeSinceStartup;
		}
	}

	public void GenerateFire() {
		for(var i=0;i < StartingFires;i++) {
			var randomX = Random.Range (0, (int)_mapSize.x);
			var randomY = Random.Range (0, (int)_mapSize.y);
			var fire = Instantiate (StarterFirePrefab).GetComponent<Fire>();

			_activeFires.Add (fire);
			Messenger<Vector2, Item>.Broadcast ("addItemToMap", new Vector2(randomX, randomY), fire);
			Messenger<Vector2, float>.Broadcast ("cameraPanToAndWait", new Vector2(randomX, randomY), 3f);
		}
	}

	void OnMapInitialized(Vector2 size) {
		_mapSize = size;
	}
}
