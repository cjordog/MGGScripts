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
			if(script.action1 != a1) {
				script.SetAction1(script.action1);
				a1 = script.action1;
			}
			if(script.action2 != a2) {
				script.SetAction2(script.action2);
				a2 = script.action2;
			}
		}
		else {
			a1 = script.action1;
			a2 = script.action2;
		}
	}
}
