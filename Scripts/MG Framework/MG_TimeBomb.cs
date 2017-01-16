using UnityEngine;
using System.Collections;

public class MG_TimeBomb : MGFramework {

	public float powerUpSpawnRate = 0.25f;
	public GameObject gameField;
	public GameObject powerup;
	float boundsX;


	void Update() {
		if (Random.value < powerUpSpawnRate)
			SpawnPowerup ();
	}

	protected override void StartMinigame ()
	{
		base.StartMinigame ();

		Mesh planeMesh = gameField.GetComponent<MeshFilter>().mesh;
		Bounds bounds = planeMesh.bounds;
		boundsX = planeMesh.bounds.size.x * 5f;

		// Spawn players
		for (int i = 0; i < players.Length; i++) {
			players [i].SpawnPoint = spawnPoints [i];
			players [i].Respawn ();
		}
	}

	void SpawnPowerup() {
		float xLoc = Random.value * boundsX - boundsX / 2;
		float zLoc = Random.value * boundsX - boundsX / 2;
		Vector3 loc = new Vector3 (xLoc, powerup.transform.localScale.y, zLoc);
		Instantiate (powerup, loc, Quaternion.identity);
	}

}