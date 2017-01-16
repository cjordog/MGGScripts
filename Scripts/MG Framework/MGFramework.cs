using UnityEngine;
using System.Collections;

public abstract class MGFramework : MonoBehaviour {

	[SerializeField] private bool useTime;
	[SerializeField] private int maxTime;
	[SerializeField] private float currentTime;

	protected bool isRunning = false;
	public bool IsRunning {
		get { return isRunning; }
	}


	private PlayerUtility[] players;
	public Vector3[] spawnPoints;

	void Start () {
		StartMinigame();
	}

	void Update () {
		if(isRunning) {
			if(useTime)
				UpdateTimer();
			UpdateUI();
			UpdateActions();
		}
	}

	void UpdateTimer() {
		if (currentTime > 0f)
			currentTime -= Time.deltaTime;
		else
			currentTime = 0f;

		if (currentTime == 0f)
			EndMinigame();
	}

	void UpdateUI() {
	}

	protected virtual void UpdateActions() {}

	void SpawnPlayers(string action1, string action2, int numPlayers) {
	}

	protected virtual bool Respawn(uint num) {
		return true;
	}

	void RulesPopup(string description,
		string action1, string action2, string leftAxis = "Move") {
	}

	protected virtual void StartMinigame() {
		// Do this after countdown
		isRunning = true;
		currentTime = maxTime;
		// Get player references
	}

	protected virtual void EndMinigame() {
		isRunning = false;
	}


}
