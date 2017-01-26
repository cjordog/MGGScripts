using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GlobalData))]
public class GlobalDataEditor : Editor {

	public override void OnInspectorGUI() {
		GlobalData script = (GlobalData)target;

		EditorGUILayout.LabelField("Number of Players: " + script.NumPlayers);

		for(int i = 0; i < script.NumPlayers; i++) {
			EditorGUILayout.LabelField("Controller Number: "
				+ script.players[i].controlNum
				+ " Use Controller: "
				+ script.players[i].useController);
		}
	}
}
