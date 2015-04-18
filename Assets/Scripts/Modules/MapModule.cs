using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapModule : BaseMonoBehaviour {
	public bool GenerateOnStart = false;

	public Vector2 MapSize = new Vector2(32, 32);
	public Vector2 TileSize = new Vector2(32, 32);
	public BaseCityGenerator CityGenerator;

	private GameObject _root;
	private Tile[,] _mapTiles; 
	private Dictionary<Vector2, Item> _items = new Dictionary<Vector2, Item>();

	// Use this for initialization
	void Awake() {
		Messenger<Vector2, Item>.AddListener ("addItemToMap", AddItemToMap);
		// Create the root
		_root = new GameObject ("Map Root");
	}

	void Start () {
		if (GenerateOnStart)
			GenerateMap ();
	}

	public Rect GetMapScreenBounds() {
		return new Rect();
	}

	public void AddItemToMap(Vector2 coords, Item i) {
		if (_mapTiles == null)
			return;
		
		i.transform.SetParent (_mapTiles [(int)coords.x, (int)coords.y].transform);
		i.transform.localPosition = Vector2.zero;
		_items.Add (new Vector2((int)coords.x, (int)coords.y), i);
	}

	Tile CreateTile(float x, float y, GameObject prefab) {
		var tile = Instantiate (prefab);

		// Set parent
		tile.transform.parent = _root.transform;

		tile.transform.localPosition = new Vector2 (x, y);

		return tile.GetComponent<Tile>();
	}

	void ClearMap() {
		foreach (var tile in _mapTiles) {
			DestroyImmediate(tile);
		}

		foreach (var item in _items.Values) {
			DestroyImmediate(item);
		}

		_mapTiles = null;
		_items.Clear ();

	}

	public void GenerateMap() {
		if (_mapTiles != null && _mapTiles.Length > 0)
			ClearMap ();

		// Some basic setup.
		var tileWidth = (TileSize.x / 2) / 100;
		var tileHeight = (TileSize.y / 2) / 100;
		_mapTiles = new Tile[(int)MapSize.x, (int)MapSize.y];

		// Center the map pre-render.
		_root.transform.localPosition = new Vector2 (MapSize.x * tileWidth * -1,
		                                             0);

		for (var x=0; x < MapSize.x; x++) {
			for(var y=0;y < MapSize.y;y++) {
				var tileX = (x * tileWidth) + (y * tileWidth);
				var tileY = (tileHeight / 2 * x) - (tileHeight / 2 * y);
				
				var tilePrefab = CityGenerator.GetTile (x, y);
				// Create the tile from prefrab.
				var tile = CreateTile (tileX, tileY, tilePrefab);
				// Name the tile.
				tile.name = string.Format ("Tile ({0},{1})", x, y);
				// Set the sorting order for correct depth buffer.
				tile.spriteRenderer.sortingOrder = y;
				// Store the tile.
				_mapTiles[x, y] = tile;
			}
		}

		Messenger<Vector2>.Broadcast ("mapInitialized", MapSize);
	}
}
