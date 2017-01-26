using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GlobalData))]
public class GlobalDataEditor : Editor {

	public override void OnInspectorGUI() {
		GlobalData script = (GlobalData)target;

		EditorGUILayout.LabelField("Number of Players: " + script.NumPlayers);
	}
}
