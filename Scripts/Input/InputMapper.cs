using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMapper : MonoBehaviour {

	private GlobalData data;
	private List<PlayerInputHandler.ControlSettings> controlSettings;

	void Start() {
		data = GameObject.FindGameObjectWithTag("Data").GetComponent<GlobalData>();
	}

	void Update() {
		controlSettings = PlayerInputHandler.GetAllAction1();
		for(int i = 0; i < controlSettings.Count; i++) {
			data.AddPlayerData(controlSettings[i]);
		}

		controlSettings = PlayerInputHandler.GetAllAction2();
		for(int i = 0; i < controlSettings.Count; i++) {
			data.RemovePlayerData(controlSettings[i]);
		}
	}
}
