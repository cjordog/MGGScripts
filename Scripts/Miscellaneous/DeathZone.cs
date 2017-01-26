using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

	[SerializeField] MGFramework framework;

	void Start() {
		framework = GameObject.FindGameObjectWithTag ("Framework").GetComponent<MGFramework>();
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			int playerNum = other.gameObject.GetComponent<PlayerUtility> ().PlayerNum;
			framework.Respawn (playerNum);
		} else {
			Destroy (other.gameObject);
		}
	}
}
