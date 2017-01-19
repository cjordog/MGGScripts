using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerActionHandler))]
[CanEditMultipleObjects]
public class PlayerActionHandlerEditor : Editor {

	private Actions.ActionType a1;
	private Actions.ActionType a2;

	public override void OnInspectorGUI() {
		DrawDefaultInspector();
		PlayerActionHandler script = (PlayerActionHandler)target;

		if(Application.isPlaying) {
			if(script.actionType1 != a1) {
				script.SetAction1(script.actionType1);
				a1 = script.actionType1;
			}
			if(script.actionType2 != a2) {
				script.SetAction2(script.actionType2);
				a2 = script.actionType2;
			}
		}
		else {
			a1 = script.actionType1;
			a2 = script.actionType2;
		}
	}
}
