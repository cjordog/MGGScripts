using UnityEngine;
using System.Collections;

public class PlayerUtility : MonoBehaviour {

	////////////////////////////////////////////
	// Player Properties
	////////////////////////////////////////////

	private Vector3 spawnPoint;
	public Vector3 SpawnPoint {
		get { return spawnPoint; }
		set { spawnPoint = value; }
	}

	private bool isDead;
	public bool IsDead {
		get { return isDead; }
		set { isDead = true; }
	}

	private float points;
	public float Points {
		get { return points; }
		set { points = value; }
	}

	private int team;
	public int Team {
		get { return team; }
		set { team = value; }
	}

	public Vector3 Location {
		get { return transform.position; }
	}

	public Vector3 Velocity {
		get { return rb.velocity; }
	}

	////////////////////////////////////////////
	// Player Components
	////////////////////////////////////////////

	private Rigidbody rb;


	void Start()
	{
		rb = gameObject.GetComponentInChildren<Rigidbody>();
	}

	public void Respawn()
	{
		gameObject.transform.position = spawnPoint;
	}
}
