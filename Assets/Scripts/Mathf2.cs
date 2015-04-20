using UnityEngine;

public static class Mathf2 {
	public static float DistanceTo(Vector2 from, Vector2 to) {
		var xd = to.x - from.x;
		var xy = to.y - from.y;

		return Mathf.Sqrt (Mathf.Pow (xd, 2) + Mathf.Pow (xy, 2));
	}

	public static float ManhattanDistanceTo(Vector2 from, Vector2 to) {
		return (to.x - from.x) + (to.y - from.y);
	}
}