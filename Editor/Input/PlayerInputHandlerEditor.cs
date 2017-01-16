using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerInputHandler))]
[CanEditMultipleObjects]
public class PlayerInputHandlerEditor : Editor {

	public override void OnInspectorGUI() {
		if(!Application.isPlaying) {
			DrawDefaultInspector();
		}
		else {
			GUILayout.Label("Player Number: " + ((PlayerInputHandler)target).playerNumber);
		}
	}
}
