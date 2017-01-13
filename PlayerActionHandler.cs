using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerActionHandler : MonoBehaviour {

	public PlayerAction action1;
	public PlayerAction action2;

	private PlayerAction[] actions;
	private PlayerMovement movement;

	#if UNITY_EDITOR
	public bool _action1 = false;
	void Update() {
		if (_action1) {
			_action1 = false;
			Action1();
		}
	}
	#endif

	void Awake() {
		action1 = new JumpAction();
		action2 = new NullAction();
		actions = new PlayerAction[2] { action1, action2 };
		movement = GetComponent<PlayerMovement>();
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

	void OnCollisionEnter(Collision col) {
		for(int i = 0; i < 2; i++)
			actions[i].collisionHandlers.Enter(gameObject, col);
	}

	void OnCollisionStay(Collision col) {
		for(int i = 0; i < 2; i++)
			actions[i].collisionHandlers.Stay(gameObject, col);
	}

	void OnCollisionExit(Collision col) {
		for(int i = 0; i < 2; i++)
			actions[i].collisionHandlers.Exit(gameObject, col);
	}

	void OnTriggerEnter(Collider other) {
		for(int i = 0; i < 2; i++)
			actions[i].triggerHandlers.Enter(gameObject, other);
	}

	void OnTriggerStay(Collider other) {
		for(int i = 0; i < 2; i++)
			actions[i].triggerHandlers.Stay(gameObject, other);
	}

	void OnTriggerExit(Collider other) {
		for(int i = 0; i < 2; i++)
			actions[i].triggerHandlers.Exit(gameObject, other);
	}
}
