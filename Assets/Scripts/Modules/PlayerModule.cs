using UnityEngine;
using System.Collections;

public class PlayerModule : MonoBehaviour {

	public int Gold;
	public float GoldChance = 0.5f;

	// Use this for initialization
	void Awake () {
		Messenger<Vector2>.AddListener ("buildingDestroyed", OnBuildingDestroyed);
		Messenger<int>.AddListener ("subtractGold", amt => Gold -= amt);
	}
	
	void OnBuildingDestroyed(Vector2 tile) {
		if (Random.Range (0f, 1f) < GoldChance) {
			Gold += Random.Range (0, 2);
		}
	}
}
