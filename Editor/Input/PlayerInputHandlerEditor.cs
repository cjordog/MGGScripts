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
			PlayerInputHandler script = (PlayerInputHandler)target;
			GUILayout.Label("Player Number: " + script.playerNumber);
			if(script.useController)
				GUILayout.Label("Player is using controller");
			else
				GUILayout.Label("Player is not using controller");
		}
	}
}
