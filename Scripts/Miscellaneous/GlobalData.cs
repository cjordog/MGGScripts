﻿using UnityEngine;
using System.Collections;

public class GlobalData : MonoBehaviour {

	//public for testing, should be private
	public int numPlayers;
	public int NumPlayers {
		get { return numPlayers; }
		set { numPlayers = value; }
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

	public int AddPlayerData(int controlNum, bool useController = false) {
		if(numPlayers < 3 && numPlayers >= 0) {
			players[numPlayers].points = 0;
			players[numPlayers].hats = new string[5];
			players [numPlayers].useController = useController;
			numPlayers++;
			return numPlayers - 1;
		}
		else {
			Debug.LogError("Maximum Players Reached: 4");
			return -1;
		}
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
