using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerInputHandler))]
[CanEditMultipleObjects]
public class PlayerInputHandlerEditor : Editor {

	private uint playerNum;
	private bool state1;
	private uint previousNum;
	private bool useController;
	private string controlString;

	public override void OnInspectorGUI() {
		PlayerInputHandler script = (PlayerInputHandler)target;
		// controlString = useController ? "Controller" : "Keyboard";

		DrawDefaultInspector();

		if(Application.isPlaying) {
			if(script.playerNumber != playerNum ||
				script.useController != useController) {
				script.Reset();
				playerNum = script.playerNumber;
				useController = script.useController;
			}
		}
		else {
			playerNum = script.playerNumber;
			useController = script.useController;
		}
	}
}
