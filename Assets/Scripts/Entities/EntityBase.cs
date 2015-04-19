using UnityEngine;
using System.Collections;

public class EntityBase : BaseMonoBehaviour {

	public Vector2 GetLocation() {
		return transform.parent.GetComponent<Tile> ().Position;
	}

	public void SetLocation(Vector2 newLocation) {

		//TODO: set location here
	}

	public Tile GetTile(){
		return transform.parent.GetComponent<Tile> ();
	}
}
