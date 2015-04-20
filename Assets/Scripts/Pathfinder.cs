using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Pathfinder {
	private readonly MapModule _map;
	private readonly EntityManager _entity;
	private readonly Direction[] _searchableDirections = new Direction[] {
		Direction.Up,
		Direction.Left,
		Direction.Right,
		Direction.Down
	};
	private readonly List<WeightedTile> _openTiles = new List<WeightedTile> ();
	private readonly List<WeightedTile> _closedTiles = new List<WeightedTile> ();

	public Pathfinder(MapModule map, EntityManager entity) {
		_map = map;
		_entity = entity;
	}

	bool IsTraversable(Vector2 coords) {
		if (coords.x >= _map.MapSize.x
		    && coords.x >= _map.MapSize.y
		    && coords.x < 0
		    && coords.x < 0) {
			return false;
		}
		return true;
	}

	public Vector2[] Navigate(Vector2 start, Vector2 destination) {
		var startTile = CalculateTileWeight (start, destination, null);
		_closedTiles.Add (startTile);

		// Start algorithm.
		NavigateRecurse (startTile, destination);

		// We're done! Get the path..
		var path = FindPath (startTile,
		                     _closedTiles.First (t => t.Position == destination));

		// Return the path.
		return path;
	}
	/// <summary>
	/// Navigates the recurse.
	/// </summary>
	/// <returns>The depth.</returns>
	/// <param name="currentTile">Current tile.</param>
	/// <param name="destination">Destination.</param>
	void NavigateRecurse(WeightedTile currentTile, Vector2 destination) {
		var localTiles = new List<WeightedTile> ();
		foreach (var direction in _searchableDirections.Select (d => d.ToVector2())) {
			if(IsTraversable(currentTile.Position + direction)) {
				// Check to see if our tile is already on the open list.
				if(_openTiles.Any(wt => wt.Position == (currentTile.Position + direction))) {
					var openTile = _openTiles.First (t => t.Position == (currentTile.Position + direction));
					localTiles.Add (openTile);
					// We could recalculate the G value here but I'm lazy...
				} else { // If not, calculate the weighted value.
					localTiles.Add (CalculateTileWeight(currentTile.Position + direction, destination, currentTile));
				}
			}
		}

		var quickestTile = localTiles.OrderByDescending (t => t.Weight).First ();
		localTiles.Remove (quickestTile);
		_closedTiles.Add (quickestTile);
		_openTiles.AddRange (localTiles);

		if (quickestTile.Position == destination) {
			return; // We're done!!
		} else {
			// Keep going...
			NavigateRecurse (quickestTile, destination);
		}
	}

	Vector2[] FindPath(WeightedTile origin, WeightedTile destination) {
		var currentNode = destination;
		var path = new List<WeightedTile> ();

		while (currentNode != origin) {
			currentNode = currentNode.Parent;
			path.Add (currentNode);
		}

		return path.Select (wt => wt.Position).ToArray();
	}

	WeightedTile CalculateTileWeight(Vector2 tile, Vector2 destination, WeightedTile parent) {
		return new WeightedTile () {
			Position = tile,
			Parent = parent,
			MovementCost = 10,
			Heuristic = Mathf.RoundToInt((Mathf.Abs (destination.x - tile.x) * 10) 
				+ (Mathf.Abs (destination.y - tile.y) * 10))
		};
	}

	private class WeightedTile {
		public WeightedTile Parent;
		public Vector2 Position;
		public int MovementCost; // G
		public int Heuristic; // H
		public double Weight { // F
			get {
				return Heuristic + MovementCost;
			}
		} 
	}
}


