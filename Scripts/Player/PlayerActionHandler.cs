using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerActionHandler : MonoBehaviour {

	public Actions.ActionType action1, action2;

	private PlayerAction _action1;
	private PlayerAction _action2;

	public PlayerAction[] actions;
	private bool[] onCooldown;

	private PlayerMovement movement;
	private PlayerInputHandler input;

	void Awake() {
		_action1 = Actions.GetAction(action1);
		_action2 = Actions.GetAction(action2);
		actions = new PlayerAction[2] { _action1, _action2 };
		onCooldown = new bool[2];
		movement = GetComponent<PlayerMovement>();
		input = GetComponent<PlayerInputHandler> ();
	}

	void Update() {
		if (input.GetAction1() && !onCooldown[0])
			Action1();
		if (input.GetAction2() && !onCooldown[1])
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
			onCooldown[num] = true;
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
		onCooldown[num] = false;
	}
}
