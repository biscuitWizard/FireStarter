using UnityEngine;
using System.Collections;

public class MapModule : BaseMonoBehaviour {

	public bool RenderOnStart = false;
	public bool GenerateOnStart = false;

	public Vector2 MapSize = new Vector2(32, 32);
	public Vector2 TileSize = new Vector2(32, 32);
	public BaseCityGenerator CityGenerator;

	private GameObject _root;

	// Use this for initialization
	void Start () {
		var tileWidth = (TileSize.x / 2) / 100;
		var tileHeight = (TileSize.y / 2) / 100;


		// Create the root and center it
		_root = new GameObject ("Map Root");
		_root.transform.localPosition = new Vector2 (MapSize.x * tileWidth * -1,
		                                             0);

		for (var x=0; x < MapSize.x; x++) {
			for(var y=0;y < MapSize.y;y++) {
				var tileX = (x * tileWidth) + (y * tileWidth);
				var tileY = (tileHeight / 2 * x) - (tileHeight / 2 * y);

				var tilePrefab = CityGenerator.GetTile (x, y);
				var tile = CreateTile (tileX, tileY, tilePrefab);
				tile.name = string.Format ("Tile ({0},{1})", x, y);
				tile.GetComponent<SpriteRenderer>().sortingOrder = y;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	GameObject CreateTile(float x, float y, GameObject prefab) {
		var tile = Instantiate (prefab);

		// Set parent
		tile.transform.parent = _root.transform;

		tile.transform.localPosition = new Vector2 (x, y);

		return tile;
	}
}
