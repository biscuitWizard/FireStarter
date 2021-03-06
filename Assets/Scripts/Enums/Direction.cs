using UnityEngine;

public enum Direction : int {
	None = 0,
	Up = 1,
	Down = 5,
	Left = 7,
	Right = 3,
	UpperLeft = 8,
	UpperRight = 2,
	LowerLeft = 6,
	LowerRight = 4
}

public static class DirectionExtensions {
	public static Vector2 ToVector2(this Direction d) {
		switch(d) {
		case Direction.None:
			return Vector2.zero;
		case Direction.Up:
			return Vector2.up;
		case Direction.Down:
			return Vector2.up * -1;
		case Direction.Left:
			return Vector2.right * -1;
		case Direction.Right:
			return Vector2.right;
		case Direction.UpperLeft:
			return (Vector2.right * -1) + Vector2.up;
		case Direction.UpperRight:
			return Vector2.up + Vector2.right;
		case Direction.LowerLeft:
			return (Vector2.right * -1) + (Vector2.up * -1);
		case Direction.LowerRight:
			return (Vector2.up * -1) + Vector2.right;
		default:
			return Vector2.zero;
		}
	}
}