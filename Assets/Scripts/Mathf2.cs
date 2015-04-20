using UnityEngine;

public static class Mathf2 {
	public static float DistanceTo(Vector2 from, Vector2 to) {
		var xd = to.x - from.x;
		var xy = to.y - from.y;

		return Mathf.Sqrt (Mathf.Pow (xd, 2) + Mathf.Pow (xy, 2));
	}
}