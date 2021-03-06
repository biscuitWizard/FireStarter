﻿using UnityEngine;
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

	void Awake() {
		_lastCheck = Time.realtimeSinceStartup;
	}

	// Update is called once per frame
	public override void GameUpdate () {
		if (Time.realtimeSinceStartup - _lastCheck > FireSpreadSecondsInterval) {
			var firesToCheck = _fires.OrderBy (f => System.Guid.NewGuid()).Take (FiresToCheckSpreadPerTick);
			foreach(var fire in firesToCheck) {
				var roll = Random.Range (0f, 1f);
				if(roll < NearbyTileFlammabilityPercent
				   && fire.CanSetOnFire()) { 
					StartFire (fire.Position + Direction.Down.ToVector2(), FireStage.Kindling);
				}
			}

			_lastCheck = Time.realtimeSinceStartup;
		}
	}

	public void GenerateFires() {
		while (_fires.Count < 2) {
			var randomX = Random.Range (0, (int)Map.MapSize.x - 1);
			var randomY = Random.Range (0, (int)Map.MapSize.y - 1);

			var tile = Map.GetTile (randomX, randomY);
			if(tile.GetFireSeverity() == FireStage.Flammable) {
				StartFire(new Vector2(randomX, randomY), FireStage.Kindling);

				//Messenger<Vector2, float>.Broadcast ("cameraPanToAndWait", new Vector2(randomX, randomY), 3f);
			}
		}
	}

	public void ExtinguishFire(Vector2 location) {
		var fire = _fires.First (f => f.Position == location);
		_fires.Remove (fire);
		fire.GetComponent<FlammableTile> ().StopFire ();
	}

	public void StartFire(Vector2 location, FireStage severity) {
		var tile = Map.GetTile ((int)location.x, (int)location.y);
		tile.SetFire (severity);
		_fires.Add (tile);
	}

	public Tile[] GetBurningTiles() {
		return _fires.ToArray ();
	}

	public Vector2 GetClosestBurningTile(Vector2 location, int distance) {

		var fires = _fires.Select (f => {
			var fireLocation = f.Position;
			var xd = fireLocation.x - location.x;
			var yd = fireLocation.y - location.y;

			return new FireDistance(f, Mathf.Sqrt(Mathf.Pow (xd, 2) + Mathf.Pow (yd,2)));
		})
			.OrderBy (fd => fd.Distance);

		if (fires.Any (fd => fd.Distance < distance)) {
			return fires.First (fd => fd.Distance < distance).Tile.Position;
		} else {
			return new Vector2(-1, -1);
		}
	}
	
	private class FireDistance {
		public Tile Tile;
		public float Distance;
		
		public FireDistance(Tile tile, float distance) {
			Tile = tile;
			Distance = distance;
		}
	}

}
