using UnityEngine;
using System.Collections;

public class PlayerUtility : MonoBehaviour {

	private Vector3 spawnPoint;

	private bool isDead;

	private float points;

	private int team;

	private Rigidbody rb;


	void Start()
	{
		rb = gameObject.GetComponentInChildren<Rigidbody>();
	}

	// Test comment

	void setSpawn(Vector3 spawn)
	{
		spawnPoint = spawn;
	}

	void respawn()
	{
		gameObject.transform.position = spawnPoint;
	}

	float getPoints()
	{
		return points;
	}

	void setPoints(float p)
	{
		points = p;
	}

	void addPoints(float p)
	{
		points += p;
	}

	Vector3 getLocation()
	{
		return transform.position;
	}

	Vector3 getSpeed()
	{
		return rb.position;
	}

	int getTeam()
	{
		return team;
	}

	void setTeam(int t)
	{
		team = t;
	}

	void setDead(bool state)
	{
		isDead = state;
	}

}
