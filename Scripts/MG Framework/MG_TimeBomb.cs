using UnityEngine;
using System.Collections;

public class MG_TimeBomb : MGFramework {

	public float powerupSpawnInterval = 0.2f;
	public GameObject gameField;
	public GameObject powerup;
	float boundsX;

	public 

	int deadPlayers;

	public Actions.ActionType[] actions = new Actions.ActionType[2];

	void Update() {
		if (isRunning)
			UpdateHealth ();
	}

	protected override void StartMinigame ()
	{
		actions[0] = Actions.ActionType.Jump;
		actions [1] = Actions.ActionType.Null;
		base.StartMinigame ();

		Mesh planeMesh = gameField.GetComponent<MeshFilter>().mesh;
		Bounds bounds = planeMesh.bounds;
		boundsX = planeMesh.bounds.size.x * 5f;

		acceleration = 80f;
		drag = 10f;
		maxSpeed = 10f;
		jumpAmount = 1000f;

		Physics.gravity =  new Vector3(0, -30f, 0);

		// Spawn players
		spawnPoints = new Vector3[maxPlayers] 
			{ new Vector3 (5, 0, 5), new Vector3 (5, 0, -5), new Vector3 (-5, 0, -5), new Vector3 (-5, 0, 5) };
		SpawnPlayers(actions);
		//SpawnPlayers(actions);

		// Start spawning powerups
		SpawnPowerup();
	}

	void SpawnPowerup() {
		float xLoc = Random.value * boundsX - boundsX / 2;
		float zLoc = Random.value * boundsX - boundsX / 2;
		Vector3 loc = new Vector3 (xLoc, powerup.transform.localScale.y, zLoc);
		Instantiate (powerup, loc, Quaternion.identity);

		if (isRunning)
			Invoke ("SpawnPowerup", powerupSpawnInterval);
	}

	void UpdateHealth() {
		for (int i = 0; i < players.Length; ++i) {
			if (players[i].Health > 0f)
				players[i].Health -= Time.deltaTime;
			else {
				players [i].Health = 0f;
			}

			if (players [i].Health <= 0f) {
				killPlayer (i);
				deadPlayers++;
			}
			if (deadPlayers == players.Length)
				EndMinigame ();
		}
	}
}