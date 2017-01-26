using UnityEngine;
using System.Collections;

public class GlobalData : MonoBehaviour {

	[SerializeField] private int numPlayers = 0;
	public int NumPlayers {
		get { return numPlayers; }
	}

	public struct PlayerData {
		public int points;
		//placeholder
		public string[] hats;
		public int controlNum;
		public bool useController;
	};

	public PlayerData[] players;

	private bool initialized = false;

	void Awake() {
		if(!initialized) {
			numPlayers = 0;
			players = new PlayerData[4];
			initialized = true;
			DontDestroyOnLoad(gameObject);
		}
	}

	public int AddPlayerData(PlayerInputHandler.ControlSettings controlSettings) {
		return AddPlayerData(controlSettings.num, controlSettings.useController);
	}

	public int AddPlayerData(int controlNum, bool useController = false) {
		// Check if player is already active, and return if it is
		for(int i = 0; i < numPlayers; i++) {
			if(players[i].controlNum == controlNum &&
				players[i].useController == useController) {
				return -1;
			}
		}

		if(numPlayers < 4 && numPlayers >= 0) {
			// Set Player data and add player
			players[numPlayers].points = 0;
			players[numPlayers].hats = new string[5];
			players[numPlayers].controlNum = controlNum;
			players [numPlayers].useController = useController;
			numPlayers++;
			return numPlayers - 1;
		}
		else {
			Debug.LogError("Maximum Players Reached: 4");
			return -1;
		}
	}

	public bool RemovePlayerData(PlayerInputHandler.ControlSettings controlSettings) {
		return RemovePlayerData(controlSettings.num, controlSettings.useController);
	}

	public bool RemovePlayerData(int controlNum, bool useController) {
		for(int i = 0; i < players.Length; i++) {
			if(controlNum == players[i].controlNum &&
				useController == players[i].useController) {
				return RemovePlayerData(i);
			}
		}
		return false;
	}

	public bool RemovePlayerData(int playerNum)
	{
		if(playerNum >= numPlayers) {
			Debug.LogError("No player exists at index: " + playerNum);
			return false;
		}

		for(int i = playerNum; i < 3; i++) {
			players[i] = players[i + 1];
		}
		numPlayers--;
		return true;
	}
}
