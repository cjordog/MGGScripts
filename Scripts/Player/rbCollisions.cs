using UnityEngine;
using System.Collections;

public class rbCollisions : MonoBehaviour {

	public PlayerActionHandler action;

	void Start()
	{
		action = GetComponentInParent<PlayerActionHandler> ();
	}

	void OnCollisionEnter(Collision col) {
		for(int i = 0; i < 2; i++)
 			action.Actions[i].collisionHandlers.Enter(gameObject, col);
	}

	void OnCollisionStay(Collision col) {
		for(int i = 0; i < 2; i++)
			action.Actions[i].collisionHandlers.Stay(gameObject, col);
	}

	void OnCollisionExit(Collision col) {
		for(int i = 0; i < 2; i++)
			action.Actions[i].collisionHandlers.Exit(gameObject, col);
	}

	void OnTriggerEnter(Collider other) {
		for(int i = 0; i < 2; i++)
			action.Actions[i].triggerHandlers.Enter(gameObject, other);
	}

	void OnTriggerStay(Collider other) {
		for(int i = 0; i < 2; i++)
			action.Actions[i].triggerHandlers.Stay(gameObject, other);
	}

	void OnTriggerExit(Collider other) {
		for(int i = 0; i < 2; i++)
			action.Actions[i].triggerHandlers.Exit(gameObject, other);
	}
}
