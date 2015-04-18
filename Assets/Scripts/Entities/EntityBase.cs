using UnityEngine;
using System.Collections;

public class EntityBase : BaseMonoBehaviour {
	public Vector2 GetLocation() {
		return transform.parent.GetComponent<Tile> ().Position;
	}
}
