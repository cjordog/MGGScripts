using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerInputHandler))]
[CanEditMultipleObjects]
public class PlayerInputHandlerEditor : Editor {
	
	private readonly string[] controlOptions = new string[2] { "Keyboard", "Controller" };

//	private GUIContent[] playerNumberOptions = new GUIContent[4] {
//		new GUIContent("1"),
//		new GUIContent("2"),
//		new GUIContent("3"),
//		new GUIContent("4")
//	};

	public override void OnInspectorGUI() {
		PlayerInputHandler script = (PlayerInputHandler)target;

		script.PlayerNumber = EditorGUILayout.IntSlider(new GUIContent("Player Number",
			"The \"number\" of the player. " +
			"Represents which controller or keyboard controls to use. " +
			"May not correspond to who is considered \"first player.\""),
			script.PlayerNumber, 1, 4);

//		script.PlayerNumber = EditorGUILayout.Popup(new GUIContent("Player Number",
//			"The \"number\" of the player. " +
//			"Represents which controller or keyboard controls to use. " +
//			"May not correspond to who is considered \"first player.\""),
//			script.PlayerNumber, playerNumberOptions);

		script.UseController = (EditorGUILayout.Popup(script.UseController ? 1 : 0,
			controlOptions) == 1) ? true : false;
	}
}
