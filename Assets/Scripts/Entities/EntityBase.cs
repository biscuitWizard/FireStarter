using UnityEngine;
using System.Collections;

public class EntityBase : BaseMonoBehaviour {
	public Sprite Icon;
	public Color PawnColor;
	public string EntityName;

	public Vector2 GetLocation() {
		return transform.parent.GetComponent<Tile> ().Position;
	}

	public Tile GetTile(){
		return transform.parent.GetComponent<Tile> ();
	}
}
