using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions {

	public enum ActionType { Null, Jump };

	public static PlayerAction GetAction(ActionType type) {
		switch(type) {
		case ActionType.Null:
			return new NullAction();
		case ActionType.Jump:
			return new JumpAction();
		default:
			return new NullAction();
		}
	}
}
