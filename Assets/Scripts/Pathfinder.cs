using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Pathfinder {
	class WeightedTile {
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

	private readonly MapModule _map;
	private readonly EntityManager _entity;
	private readonly Direction[] _searchableDirections = new Direction[] {
		Direction.Up,
		Direction.Left,
		Direction.Right,
		Direction.Down
	};

	public Pathfinder(MapModule map, EntityManager entity) {
		_map = map;
		_entity = entity;
	}
	
	public Vector2[] Navigate(Vector2 start, Vector2 end) {
		var startTile = new WeightedTile() {
			Position = start
		};
		var closedList = new List<WeightedTile>();
		var openList = new List<WeightedTile>() {
			startTile
		};
		
		while(openList.Any()) {
			var quickestTile = openList.OrderBy(t => t.Weight).First();
			if(quickestTile.Position == end) { // We're done! Found the path.
				return FindPath(startTile, quickestTile);
			}
			
			openList.Remove(quickestTile);
			closedList.Add(quickestTile);
			foreach(var neighbor in _searchableDirections.Select (d => d.ToVector2())
			        .Select (d => quickestTile.Position + d)
			        .Where (d => IsTraversable(d))) {
				if(closedList.Any(c => c.Position == neighbor)) {
					continue;
				}
				
				var movementCost = Mathf.RoundToInt(
					Mathf2.DistanceTo(neighbor, startTile.Position)
					+ quickestTile.MovementCost);
				
				var existingNeighbor = openList.FirstOrDefault(o => o.Position == neighbor);
				if(existingNeighbor == null) {
					openList.Add(new WeightedTile() {
						MovementCost = movementCost,
						Heuristic = Mathf.RoundToInt(Mathf2.DistanceTo(neighbor, end)),
						Parent = quickestTile,
						Position = neighbor
					});
				} else if(existingNeighbor.MovementCost > movementCost) {
					existingNeighbor.MovementCost = movementCost;
					existingNeighbor.Parent = quickestTile;
				}				
			}
		}
		
		return new Vector2[0];
	}
	
	Vector2[] FindPath(WeightedTile start, WeightedTile end) {
		var currentNode = end;
		var path = new List<WeightedTile> ();
		
		while (currentNode.Position != start.Position) {
			currentNode = currentNode.Parent;
			path.Add (currentNode);
		}
		
		return path.Select (wt => wt.Position).Reverse().ToArray();
	}

	bool IsTraversable(Vector2 coords) {
		// Check map boundaries...
		if (coords.x >= _map.MapSize.x
		    || coords.x >= _map.MapSize.y
		    || coords.x < 0
		    || coords.x < 0) {
			return false;
		}
		return true;
	}
}