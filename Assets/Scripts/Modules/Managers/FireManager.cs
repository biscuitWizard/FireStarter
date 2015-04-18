using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(WeatherManager))]
public class FireManager : BaseMonoBehaviour {

	public GameObject StarterFirePrefab;
	public float NearbyTileFlammabilityPercent = .2f;
	public int FireSpreadSecondsInterval = 3;
	public int FiresToCheckSpreadPerTick = 2;
	public int StartingFires = 2;
	private WeatherManager _weather;
	private Vector2 _mapSize;
	private float _lastCheck;
	//private readonly List<Fire> _activeFires = new List<Fire>();

	// Use this for initialization
	void Awake () {
		Messenger<Vector2>.AddListener ("mapInitialized", OnMapInitialized);
		_weather = GetComponent<WeatherManager> ();
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
			//var fire = Instantiate (StarterFirePrefab).GetComponent<Fire>();

			//_activeFires.Add (fire);
			Messenger<Vector2, float>.Broadcast ("cameraPanToAndWait", new Vector2(randomX, randomY), 3f);
		}
	}

	public void ExtinguishFire(Vector2 location) {
		//var fire = _activeFires.First (f => f.Location == location);
		//_activeFires.Remove (fire);
		//Messenger<Vector2, Item>.Broadcast ("removeItemFromMap", location, fire);
	}

	void OnMapInitialized(Vector2 size) {
		_mapSize = size;
	}
}
