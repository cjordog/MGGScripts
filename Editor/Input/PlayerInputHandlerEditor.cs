﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerInputHandler))]
[CanEditMultipleObjects]
public class PlayerInputHandlerEditor : Editor {
	
	private readonly string[] controlOptions = new string[2] { "Keyboard", "Controller" };
	private readonly string[] playerNumOptions = new string[4] { "1", "2", "3", "4" };

	public override void OnInspectorGUI() {
		PlayerInputHandler script = (PlayerInputHandler)target;

		// Player Number Style
		GUIStyle centeredLabel = new GUIStyle(GUI.skin.label);
		centeredLabel.alignment = TextAnchor.MiddleCenter;
		centeredLabel.fontStyle = FontStyle.BoldAndItalic;

		// Player Number Label
		EditorGUILayout.LabelField(new GUIContent("Player Number",
			"The \"number\" of the player. " +
			"Represents which controller or keyboard controls to use. " +
			"May not correspond to who is considered \"first player.\""), centeredLabel);

		// Player Select
		EditorGUILayout.BeginHorizontal();
		script.PlayerNumber = GUILayout.SelectionGrid(script.PlayerNumber - 1, playerNumOptions, 4) + 1;
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.Space();

		// Control Method Select
		EditorGUILayout.BeginHorizontal();
		script.UseController = GUILayout.SelectionGrid(script.UseController ? 1 : 0,
			controlOptions, 2) == 1 ? true : false;
		EditorGUILayout.EndHorizontal();
	}
}
