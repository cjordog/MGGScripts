using UnityEngine;
using System.Collections;
using MGG;

public abstract class MGFramework : MonoBehaviour {

	[SerializeField] protected bool useTime;
	[SerializeField] protected int maxTime;
	[SerializeField] protected float currentTime;

	protected bool isRunning = false;
	public bool IsRunning {
		get { return isRunning; }
	}

	protected PlayerUtility[] players;
	protected int[] playerPlaces;
	protected int nextPlayerPlace;
	public Vector3[] spawnPoints = new Vector3[4];
	protected int numPlayers;
	protected GlobalData data;
	protected bool canRespawn = true;

	protected const int maxPlayers = 4;

	public float acceleration, drag, maxSpeed, jumpAmount;
	public Vector3 gravity = new Vector3(0f, -30f, 0f);

	public GameObject playerPrefab;

	void Start () {

		//variable initialization
		data = GameObject.FindGameObjectWithTag ("Data").GetComponent<GlobalData> ();

//		//following lines only for testing, remove later
//		data.AddPlayerData (data.NumPlayers, false);
//		data.AddPlayerData (data.NumPlayers, false);
//		//end of testing lines

		numPlayers = data.NumPlayers;
		players = new PlayerUtility[numPlayers];
		playerPlaces = new int[numPlayers];
		nextPlayerPlace = numPlayers;

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

	//allows you to edit movement variables in the inspector during runtime.
	void OnValidate() {
		for (int i = 0; i < numPlayers; i++) {
			//set PlayerMovement to minigame settings
			PlayerMovement mv = players[i].gameObject.GetComponent<PlayerMovement>();
			setMovementVariables (mv, acceleration, drag, maxSpeed, jumpAmount);
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

	protected void SpawnPlayers(PlayerActions.ActionType[] actions) {
		for (int i = 0; i < numPlayers; i++) {
			
			Debug.Log ("Spawning player: " + i + ".");
			//create player object
			GameObject player = (GameObject)Instantiate (playerPrefab, spawnPoints [i], Quaternion.identity);

			//initialize controls
			PlayerInputHandler ih = player.GetComponent<PlayerInputHandler> ();
			PlayerActionHandler ah = player.GetComponent<PlayerActionHandler> ();
			ih.ControlNumber = i+1;
			ih.UseController = data.players [i].useController;
			ah.SetActions (actions [0], actions [1]);

			//set playerUtility
			players[i] = player.GetComponent<PlayerUtility> ();

			//TODO:handle team allocation here later. passing in as i for now
			players [i].initializeVariables (spawnPoints [i], 0, players [i].healthcap, i, i);

			//set PlayerMovement to minigame settings
			PlayerMovement mv = player.GetComponent<PlayerMovement>();
			setMovementVariables (mv, acceleration, drag, maxSpeed, jumpAmount);
		}
	}

	public virtual bool Respawn(int num) {
		//num is 0-3 inclusive
		if (canRespawn && players [num].NumLives != 0) {
			players [num].Respawn ();
			players [num].NumLives--;
		} else {
			killPlayer (num);
			return false;
		}
		if (players [num].NumLives == 0) {
			killPlayer (num);
			return false;
		}
		return true;
	}

	/// <summary>
	/// Ruleses the popup.
	/// </summary>
	/// <param name="description">Description.</param>
	/// <param name="action1">Action1.</param>
	/// <param name="action2">Action2.</param>
	/// <param name="leftAxis">Left axis.</param>
	void RulesPopup(string description,
		string action1, string action2, string leftAxis = "Move") {
	}

	/// <summary>
	/// Starts the minigame.
	/// </summary>
	protected virtual void StartMinigame() {
		// Do this after countdown
		isRunning = true;
		currentTime = maxTime;
		// Get player references
	}

	/// <summary>
	/// Ends the minigame.
	/// </summary>
	protected virtual void EndMinigame() {
		//TODO::update player info back to globaldata
		isRunning = false;
		Debug.Log("Player Win");
	}

	/// <summary>
	/// Kills the player.
	/// </summary>
	/// <param name="playerNum">Player number.</param>
	protected virtual void killPlayer(int playerNum) {
		//players indexed 0-3
		players[playerNum].gameObject.SetActive(false);
	}

	//set to -1 for infinite lives
	/// <summary>
	/// Sets the player lives.
	/// </summary>
	/// <param name="lives">Lives.</param>
	protected void setPlayerLives(int lives)
	{
		for (int i = 0; i < numPlayers; i++) {
			players [i].NumLives = lives;
		}
	}

	protected virtual void setMovementVariables(PlayerMovement pm, float accel, float drag, float maxSpeed, float jumpHeight)
	{
		pm.acceleration = accel;
		pm.drag = drag;
		pm.maxSpeed = maxSpeed;
		pm.jumpAmount = jumpHeight;
	}
}
