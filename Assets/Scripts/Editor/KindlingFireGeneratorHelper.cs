using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(KindlingFireGenerator))]
public class KindlingFireGeneratorHelper : Editor {
	public override void OnInspectorGUI() {
		var generator = (KindlingFireGenerator)target;
		if(GUILayout.Button ("Generate Again")) {
			generator.GenerateFlames();
		}
	}
}
