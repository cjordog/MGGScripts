using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerUtility : MonoBehaviour {

	////////////////////////////////////////////
	// Player Properties
	////////////////////////////////////////////

	private Vector3 spawnPoint;
	public Vector3 SpawnPoint {
		get { return spawnPoint; }
		set { spawnPoint = value; }
	}

	private int playerNum;
	public int PlayerNum {
		get { return playerNum; }
		set { playerNum = value; }
	}

	private bool isDead;
	public bool IsDead {
		get { return isDead; }
		set { isDead = true; }
	}

	private int numLives;
	public int NumLives {
		get { return numLives; }
		set { numLives = value; }
	}

	private float points;
	public float Points {
		get { return points; }
		set { points = value; }
	}
		
	[Tooltip("How much health the player is allowed to have.")]
	[SerializeField] public float healthcap = 100;
	private float health;
	public float Health {
		get { return health; }
		set { health = value; }
	}

	public void addHealth(float value) {
		if (health > 0 && health < healthcap)
			health += value;
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
		rb.velocity = Vector3.zero;
		gameObject.transform.position = spawnPoint;
	}

	public void initializeVariables(Vector3 spawnPoint, float points, float health, int team)
	{
		SpawnPoint = spawnPoint;
		Points = points;
		Health = health;
		Team = team;
	}
}
