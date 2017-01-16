using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerActionHandler))]
[CanEditMultipleObjects]
public class PlayerActionHandlerEditor : Editor {

	public override void OnInspectorGUI() {
		if(Application.isPlaying) {
			PlayerActionHandler script = (PlayerActionHandler)target;
			GUILayout.Label("Action 1: " + script.action1);
			GUILayout.Label("Action 2: " + script.action2);
		}
		else {
			DrawDefaultInspector();
		}
	}
}
