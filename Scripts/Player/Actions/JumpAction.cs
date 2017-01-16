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
		RegisterCollisionExit(LeaveGround);
	}

	public override void StartAction(PlayerMovement movement) {
		Debug.Log(CanJump);
		Debug.Log("g: " + grounded + " j: " + jumping);
		if(CanJump) {
			movement.Jump();
			grounded = false;
			jumping = true;
			RegisterCollisionStay(LandOnGround, false);
		}
	}

	public override void EndAction(PlayerMovement movement) {
		jumping = false;
		RegisterCollisionStay(LandOnGround);
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
