using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGG {

	public class PlayerActions {

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
}
