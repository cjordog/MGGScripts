using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAction : PlayerAction {

	private const float JUMP_DELAY = 0.5f;

	private bool grounded;
	private bool jumping = false;

	public bool CanJump {
		get { return grounded && !jumping; }
	}

	private int groundLayer = LayerMask.NameToLayer("Ground");

	public JumpAction() : base(JUMP_DELAY) {
		RegisterCollisionStay(LandOnGround);
	}

	public override void StartAction(PlayerMovement movement) {
		Debug.Log(CanJump);
		Debug.Log("g: " + grounded + " j: " + jumping);
		if(CanJump) {
			movement.AddForce(new Vector3(0, 1000, 0));
			grounded = false;
			jumping = true;
			RegisterCollisionStay(LandOnGround, false);
			RegisterCollisionExit(LeaveGround);
		}
	}

	public override void EndAction(PlayerMovement movement) {
		jumping = false;
		RegisterCollisionStay(LandOnGround);
		RegisterCollisionExit(LeaveGround, false);
	}

	void LandOnGround(GameObject sender, Collision col) {
		if(col.gameObject.layer == groundLayer) {
			grounded = true;
		}
	}

	void LeaveGround(GameObject sender, Collision col) {
		if(col.gameObject.layer == groundLayer) {
			grounded = false;
		}
	}
}
