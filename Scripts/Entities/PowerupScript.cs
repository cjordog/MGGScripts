using UnityEngine;
using System.Collections;

public class PowerupScript : MonoBehaviour {

	private float powerupHealth = 1f;

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			other.gameObject.GetComponentInParent <PlayerUtility> ().addHealth (powerupHealth);
			Destroy (gameObject);
		}
	}
}