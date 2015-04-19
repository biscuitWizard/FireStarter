using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(WeatherManager))]
public class FireManager : BaseMonoBehaviour {

	public GameObject StarterFirePrefab;
	public WeatherManager WeatherManager;
	public MapModule Map;
	public float NearbyTileFlammabilityPercent = .2f;
	public int FireSpreadSecondsInterval = 3;
	public int FiresToCheckSpreadPerTick = 2;
	public int StartingFires = 2;
	
	private float _lastCheck;
	private readonly List<Tile> _fires = new List<Tile>();


	// Update is called once per frame
	public override void GameUpdate () {
		if ((_lastCheck - Time.realtimeSinceStartup) >= FireSpreadSecondsInterval) {


			_lastCheck = Time.realtimeSinceStartup;
		}
	}

	public void GenerateFires() {
		while (_fires.Count < 2) {
			var randomX = Random.Range (0, (int)Map.MapSize.x);
			var randomY = Random.Range (0, (int)Map.MapSize.y);

			var tile = Map.GetTile (randomX, randomY);
			if(tile.GetFireSeverity() == FireStage.Flammable) {
				StartFire(new Vector2(randomX, randomY), FireStage.Kindling);

				//Messenger<Vector2, float>.Broadcast ("cameraPanToAndWait", new Vector2(randomX, randomY), 3f);
			}
		}
	}

	public void ExtinguishFire(Vector2 location) {
		//var fire = _activeFires.First (f => f.Location == location);
		//_activeFires.Remove (fire);
		//Messenger<Vector2, Item>.Broadcast ("removeItemFromMap", location, fire);
	}

	public void StartFire(Vector2 location, FireStage severity) {
		var tile = Map.GetTile ((int)location.x, (int)location.y);
		tile.SetFire (severity);
		_fires.Add (tile);
	}

	public Tile[] GetBurningTiles() {
		return _fires.ToArray ();
	}
}
