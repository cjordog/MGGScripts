using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerActionHandler : MonoBehaviour {

	public Actions.ActionType action1, action2;

	private PlayerAction _action1;
	private PlayerAction _action2;

	public PlayerAction[] actions;
	private PlayerMovement movement;
	private PlayerInputHandler input;

	void Awake() {
		_action1 = Actions.GetAction(action1);
		_action2 = Actions.GetAction(action2);
		actions = new PlayerAction[2] { _action1, _action2 };
		movement = GetComponent<PlayerMovement>();
		input = GetComponent<PlayerInputHandler> ();
	}

	void Update() {
		if (input.GetAction1 ())
			Action1 ();
		if (input.GetAction2 ())
			Action2 ();
	}

	public void Action1() {
		StartAction(0);
	}

	public void Action2() {
		StartAction(1);
	}

	private void StartAction(int num) {
		Debug.Assert(num == 0 || num == 1);
		actions[num].StartAction(movement);
		if(actions[num].Duration != Mathf.Infinity) {
			Debug.Assert(actions[num].Duration >= 0);
			StartCoroutine(DelayEndAction(num, actions[num].Duration));
		}
	}

	private void EndAction(int num) {
		Debug.Assert(num == 0 || num == 1);
		actions[num].EndAction(movement);
	}

	private IEnumerator DelayEndAction(int num, float delay) {
		yield return new WaitForSeconds(delay);
		EndAction(num);
	}
}
