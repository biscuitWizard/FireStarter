﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour {

	public SpriteRenderer spriteRenderer;
	public Vector2 Position { get; private set; }
	public TileState[] ActiveStates;
	private readonly IList<TileState> _states = new List<TileState>();

	// Use this for initialization
	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	public void SetTilePosition(Vector2 position) {
		name = string.Format ("Tile {0},{1}", position.x, position.y);
		Position = position;
	}

	public void AddTileState(TileState state) {

	}

	public void RemoveTileState(TileState state) {

	}
}