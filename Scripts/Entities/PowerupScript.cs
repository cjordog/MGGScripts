using UnityEngine;
using System.Collections;

public class PowerupScript : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		Debug.Log ("collide");
		if (other.tag == "Player")
			Destroy (gameObject);
	}
}
