using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Tile))]
public class TileRenderer : MonoBehaviour {
	public Tile TileInfo;
	public TileIcon[] TileStates;

}

[Serializable]
public struct TileIcon {
	public string Name;
	public TileState State;
}