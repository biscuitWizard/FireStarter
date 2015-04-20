#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Tile))]
public class BurningDebug : Editor {

	public override void OnInspectorGUI() {
		DrawDefaultInspector ();

		var tile = (Tile)target;
		if(GUILayout.Button ("Set On Fire")) {
			Debug.Log ("Clicked button!");
			tile.SetFire (FireStage.Kindling);
		}
	}
}
#endif