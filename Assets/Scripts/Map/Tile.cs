using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour {

	public SpriteRenderer spriteRenderer;
	public Vector2 Position { get; private set; }

	// Use this for initialization
	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	public void SetTilePosition(Vector2 position) {
		name = string.Format ("Tile {0},{1}", position.x, position.y);
		Position = position;
	}
}
