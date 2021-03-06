﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerMovement : MonoBehaviour {

	float vert;
	float horiz;

	[Tooltip("How quickly the player accelerates.")]
	public float acceleration = 15;
	[Tooltip("How quickly the player decelerates.")]
	public float drag = 10;
	[Tooltip("Max speed that the player can reach (in xz-plane).")]
	public float maxSpeed = 15;

	[Tooltip("Used for easy monitoring of current player speed. " +
			 "\n Do not edit this value.")]
	public float currSpeed;
	public float currSpeedXZ;

	[Tooltip("How much the player model tilts in the direction of movement.")]
	public float tilt = 8;

	//minimum velocity before velocity is set to 0
	float velocityThreshhold = 0.5f;

	Rigidbody rb;
	PlayerInputHandler input;
	public GameObject model;

	public float jumpAmount = 10;

	Vector3 rbOffset;


	void Start()
	{
		rb = GetComponentInChildren<Rigidbody> ();
		input = GetComponent<PlayerInputHandler>();

		//record where the rigidbody sits relative to its parent
		rbOffset = rb.transform.position - gameObject.transform.position;
	}
		

	void FixedUpdate () {


		vert = input.GetVertical();
		horiz = input.GetHorizontal();

		//controls player movement
		rb.AddForce (acceleration * (new Vector3 (horiz, 0f, vert)));
//		rb.AddTorque (acceleration * 100 * (new Vector3 (vert, 0f, -horiz)));

		//get current velocity, global and in xz plane
		currSpeed = rb.velocity.magnitude;
		currSpeedXZ = (new Vector2(rb.velocity.x, rb.velocity.z)).magnitude;

		//cap velocity to maxSpeed
		if (currSpeedXZ >= maxSpeed) {
			//keep y component of velocity seperate in order to allow gravity to still work correctly.
			float yVel = rb.velocity.y;
			Vector3 newVel = rb.velocity;
			newVel.y = 0;
			newVel = newVel.normalized * maxSpeed;
			newVel.y = yVel;
			rb.velocity = newVel;
		}

		//if no player input
		if (vert == 0 && horiz == 0) {
			//set speed to 0 if its below threshold
			if (currSpeedXZ < velocityThreshhold) {
				rb.velocity = new Vector3 (0, rb.velocity.y, 0);
			}
			//implementing custom deceleration for player
			//**Problem with unity drag is that we only want drag to act on the 
			//**player when no input is occuring, otherwise its too hard to move
			Vector3 dragForce = -rb.velocity * drag;
			dragForce.y = 0;
			rb.AddForce(dragForce);
		}

		//recalculate capped speed
		currSpeed = rb.velocity.magnitude;
		currSpeedXZ = (new Vector2(rb.velocity.x, rb.velocity.z)).magnitude;

		//make model tilt in the direction of motion
		if (currSpeed > velocityThreshhold) {
			model.transform.rotation = Quaternion.Euler (Mathf.Max (Mathf.Abs (vert * tilt), Mathf.Abs (horiz * tilt)), 
				Quaternion.LookRotation (rb.velocity).eulerAngles.y, 0);
		}
	}


	void Update()
	{
		//update player object position
		transform.position = rb.transform.position - rbOffset;

		//update rigidbody position
		//NOTE: when rigidbody position is set, it is relative to the scene, NOT ITS PARENT OBJECT
		rb.transform.position = transform.position + rbOffset;
	}

	// Temporary Function
	public void AddForce(Vector3 force) {
		rb.AddForce(force);
	}

	public void Jump()
	{
		rb.AddForce (0, jumpAmount, 0);
	}
}
