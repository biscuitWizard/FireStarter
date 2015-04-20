using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PathfinderAStar  {
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

	private readonly MapModule _map;
	private readonly EntityManager _entity;
	private readonly Direction[] _searchableDirections = new Direction[] {
		Direction.Up,
		Direction.Left,
		Direction.Right,
		Direction.Down
	};

	public PathfinderAStar(MapModule map, EntityManager entity) {
		_map = map;
		_entity = entity;
	}

	public Vector2[] Navigate(Vector2 start, Vector2 end) {
		var startTile = new WeightedTile () {
			Position = start
		};
		var openList = new List<WeightedTile> ();
		var closedList = new List<WeightedTile> ();

		openList.Add (startTile);

		while (openList.Count != 0) {
			var quickestTile = openList.OrderByDescending(t => t.Weight).First ();
			openList.Remove(quickestTile); // pop q off the list

			var successors = new List<WeightedTile>();
			foreach (var direction in _searchableDirections
			         .Select (d => d.ToVector2())
			         .Where (d => IsTraversable(quickestTile.Position + d))) {
				successors.Add (CalculateTileWeight(quickestTile.Position + direction,
				                                  end, quickestTile));
			}

			foreach(var successor in successors) {
				if(successor.Position == end) { // End goal!
					break;
				}

				if(openList.Any (op => op.Position == successor.Position
				                 && op.Weight < successor.Weight)) {
					continue;
				}

				if(closedList.Any (op => op.Position == successor.Position
				                   && op.Weight < successor.Weight)) {
					continue;
				}

				openList.Add (successor);
			}

			closedList.Add (quickestTile);
		}

		var path = FindPath (startTile,
		                     closedList.First (t => t.Position == end));

		return path.Reverse ().ToArray ();
	}

	Vector2[] FindPath(WeightedTile origin, WeightedTile destination) {
		var currentNode = destination;
		var path = new List<WeightedTile> ();
		
		while (currentNode.Position != origin.Position) {
			currentNode = currentNode.Parent;
			path.Add (currentNode);
		}
		
		return path.Select (wt => wt.Position).ToArray();
	}

	WeightedTile CalculateTileWeight(Vector2 tile, Vector2 destination, WeightedTile parent) {
		return new WeightedTile () {
			Position = tile,
			Parent = parent,
			MovementCost = Mathf.RoundToInt(Mathf2.DistanceTo(tile, parent.Position) 
			                                + parent.MovementCost),
			Heuristic = Mathf.RoundToInt((Mathf.Abs (destination.x - tile.x) * 10) 
			                             + (Mathf.Abs (destination.y - tile.y) * 10))
		};
	}

	bool IsTraversable(Vector2 coords) {
		//if (coords.x >= _map.MapSize.x
		//    || coords.x >= _map.MapSize.y
		//    || coords.x < 0
		//    || coords.x < 0) {
		//	return false;
		//}
		return true;
	}
}
