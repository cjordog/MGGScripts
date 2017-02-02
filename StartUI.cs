using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour {

	private Text[] playerTexts;
	private GameObject startgui;
	private int numPlayersRecorded;
	private GlobalData data;

	// Use this for initialization
	void Start () {
		data = GameObject.FindGameObjectWithTag ("Data").GetComponentInChildren<GlobalData>();
		startgui = GameObject.FindGameObjectWithTag ("StartUI");
		playerTexts = startgui.GetComponentsInChildren<Text> ();
		numPlayersRecorded = 0;
	}
	
	// Update is called once per frame
	void Update () {
		int playerNumDiff = data.NumPlayers - numPlayersRecorded;
		//if players were added
		if (playerNumDiff > 0){
			for (int j = 0; j < playerNumDiff; j++) {
				playerTexts [numPlayersRecorded + j].text = "Joined.";
			}
		}
		//if players were removed
		if (playerNumDiff < 0) {
			for (int j = -1; j >= playerNumDiff; j--) {
				playerTexts [numPlayersRecorded + j].text = "Press A to Join";
			}
		}
		numPlayersRecorded = data.NumPlayers;

		if (Input.GetKeyDown ("return"))
			SceneManager.LoadScene ("Scenes/TimeBomb");
	}
}
