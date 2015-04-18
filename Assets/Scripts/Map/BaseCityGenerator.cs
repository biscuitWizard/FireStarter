using UnityEngine;
using System.Collections;

public abstract class BaseCityGenerator : BaseMonoBehaviour {

	public abstract GameObject GetTile (int x, int y);
}
