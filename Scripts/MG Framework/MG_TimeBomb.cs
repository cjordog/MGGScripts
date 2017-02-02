using UnityEngine;
using System.Collections;
using MGG;

public class MG_TimeBomb : MGFramework {

	public float powerupSpawnInterval = 0.2f;
	public GameObject gameField;
	public GameObject powerup;
	float boundsX;

	int deadPlayers;

	public PlayerActions.ActionType[] actions = new PlayerActions.ActionType[2];

	protected override void GameLoop() {
		UpdateHealth ();
		if(CheckForPlayerWin()) {
			EndMinigame();
		}
	}

	protected override void StartMinigame ()
	{
		actions[0] = PlayerActions.ActionType.Jump;
		actions [1] = PlayerActions.ActionType.Null;
		base.StartMinigame ();

		Mesh planeMesh = gameField.GetComponent<MeshFilter>().mesh;
		Bounds bounds = planeMesh.bounds;
		boundsX = planeMesh.bounds.size.x * 5f;

		canRespawn = false;

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

		setPlayerLives (1);

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
		for (int i = 0; i < numPlayers; ++i) {
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

	bool CheckForPlayerWin() {
		int playersDied = 0;
		for(int i = 0; i < players.Length; i++) {
			if(players[i].IsDead && playerPlaces[i] <= 0) {
				playerPlaces[i] = nextPlayerPlace;
				playersDied++;
			}
		}

		if(playersDied > 1) {
			for(int i = 0; i < players.Length; i++) {
				if(playerPlaces[i] == nextPlayerPlace) {
					playerPlaces[i] += playersDied - 1;
				}
			}
		}

		nextPlayerPlace += playersDied;

		if(nextPlayerPlace <= 0) {
			return true;
		}
		return false;
	}
}