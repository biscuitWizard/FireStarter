using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class TestCityGenerator : BaseCityGenerator {

	public GameObject[] TilePrefabs;

	public override GameObject GetTile(int x, int y) {
		return TilePrefabs.OrderBy(_ => Guid.NewGuid()).First();
	}
}
