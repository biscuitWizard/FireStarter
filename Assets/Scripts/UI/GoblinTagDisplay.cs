using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GoblinTagDisplay : MonoBehaviour {

	public GameObject NameTagPrefab;
	public Vector2 StartPosition = new Vector2(-80, -35);
	public float YIncrement = -55;

	void Awake() {
		Messenger<GoblinEntity>.AddListener ("goblinCreated", OnGoblinCreated);
		Messenger<GoblinEntity>.AddListener ("goblinDestroyed", OnGoblinDestroyed);
	}

	void OnGoblinCreated(GoblinEntity goblin) {
		var tag = Instantiate (NameTagPrefab).GetComponent<GoblinNameTag>();
		tag.transform.SetParent (transform);

		var childrenCount = GetComponentsInChildren<GoblinNameTag> ().Count () - 1;

		// Set position...
		var rectTransform = tag.GetComponent<RectTransform> ();
		rectTransform.anchoredPosition = new Vector2 (StartPosition.x,
		                                           StartPosition.y + (YIncrement * childrenCount));

		// Set goblin name...
		tag.SetGoblin (goblin);
	}

	void OnGoblinDestroyed(GoblinEntity goblin) {
		var nameTag = GetComponentsInChildren<GoblinNameTag> ().FirstOrDefault (nt => nt.GetGoblin () == goblin);

		if (nameTag != null) {
			DestroyImmediate(nameTag.gameObject);
		}
	}
}
