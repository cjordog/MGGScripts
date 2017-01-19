using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerActionHandler : MonoBehaviour {

	public Actions.ActionType actionType1, actionType2;

	private PlayerAction action1;
	private PlayerAction action2;

	public PlayerAction[] actions;

	private PlayerMovement movement;
	private PlayerInputHandler input;

	void Awake() {
		if(actionType1 == actionType2) {
			action1 = action2 = Actions.GetAction(actionType1);
		}
		else {
			action1 = Actions.GetAction(actionType1);
			action2 = Actions.GetAction(actionType2);
		}
		actions = new PlayerAction[2] { action1, action2 };
		movement = GetComponent<PlayerMovement>();
		input = GetComponent<PlayerInputHandler> ();
	}

	void Update() {
		if (!action1.OnCooldown && input.GetAction1())
			Action1();
		if (!action2.OnCooldown && input.GetAction2())
			Action2();
	}

	public void Action1() {
		StartAction(0);
	}

	public void Action2() {
		StartAction(1);
	}

	public void SetAction1(Actions.ActionType type) {
		SetAction(type, 0);
	}

	public void SetAction2(Actions.ActionType type) {
		SetAction(type, 1);
	}

	public void SetActions(Actions.ActionType action1Type,
		Actions.ActionType action2Type) {
		SetAction1(action1Type);
		SetAction2(action2Type);
	}

	private void SetAction(Actions.ActionType type, uint num) {
		Debug.Assert(num == 0 || num == 1);
		actions[num] = Actions.GetAction(type);
	}

	private void StartAction(int num) {
		Debug.Assert(num == 0 || num == 1);
		actions[num].StartAction(movement);
		if(actions[num].Duration != Mathf.Infinity) {
			Debug.Assert(actions[num].Duration >= 0);
			StartCoroutine(DelayEndAction(num, actions[num].Duration));
		}
		if(actions[num].Cooldown != 0) {
			Debug.Assert(actions[num].Cooldown > 0);
			actions[num].OnCooldown = true;
			StartCoroutine(OffCooldown(num, actions[num].Cooldown));
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

	private IEnumerator OffCooldown(int num, float delay) {
		yield return new WaitForSeconds(delay);
		actions[num].OnCooldown = false;
	}
}
