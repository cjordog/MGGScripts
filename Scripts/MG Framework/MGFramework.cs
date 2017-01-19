using UnityEngine;
using System.Collections;

public abstract class MGFramework : MonoBehaviour {

	[SerializeField] protected bool useTime;
	[SerializeField] protected int maxTime;
	[SerializeField] protected float currentTime;

	protected bool isRunning = false;
	public bool IsRunning {
		get { return isRunning; }
	}

	protected PlayerUtility[] players;
	public Vector3[] spawnPoints;
	protected int numPlayers;
	public GlobalData data;

	protected const int maxPlayers = 4;

	public float acceleration, drag, maxSpeed, jumpAmount;
	public Vector3 gravity;

	public GameObject playerPrefab;

	void Start () {
		//variable initialization
		data = GameObject.FindGameObjectWithTag ("Data").GetComponent<GlobalData> ();
		numPlayers = data.NumPlayers;

		StartMinigame();
	}

	void Update () {
		if(isRunning) {
			if(useTime)
				UpdateTimer();
			UpdateUI();
			GameLoop();
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

	protected virtual void GameLoop() {
		return;
	}

	protected void SpawnPlayers(Actions.ActionType[] actions) {
		for (int i = 0; i < numPlayers; i++) {
			//create player object
			GameObject player = (GameObject)Instantiate (playerPrefab, spawnPoints [i], Quaternion.identity);

			//initialize controls
			PlayerInputHandler ih = player.GetComponent<PlayerInputHandler> ();
			PlayerActionHandler ah = player.GetComponent<PlayerActionHandler> ();
			ih.PlayerNumber = i+1;
			ih.UseController = data.players [i].useController;
			ah.SetActions (actions [0], actions [1]);

			//set playerUtility
			players[i] = player.GetComponent<PlayerUtility> ();

			//TODO:handle team allocation here later. passing in as i for now
			players [i].initializeVariables (spawnPoints [i], 0, players [i].healthcap, i);

			//set PlayerMovement to minigame settings
			PlayerMovement mv = player.GetComponent<PlayerMovement>();
			setMovementVariables (mv, acceleration, drag, maxSpeed, jumpAmount);
		}
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

	protected virtual void killPlayer(int playerNum) {
		//fill later
	}

	protected virtual void setMovementVariables(PlayerMovement pm, float accel, float drag, float maxSpeed, float jumpHeight)
	{
		pm.acceleration = accel;
		pm.drag = drag;
		pm.maxSpeed = maxSpeed;
		pm.jumpAmount = jumpHeight;
	}
}
