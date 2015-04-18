using UnityEngine;
using System.Collections;

public class EntityBase : BaseMonoBehaviour {
	Vector2 GetLocation() {
		return transform.parent.GetComponent<Tile> ().Position;
	}
}
