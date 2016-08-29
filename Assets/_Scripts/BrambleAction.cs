using UnityEngine;
using System.Collections;

public class BrambleAction : MonoBehaviour {

	void OnTriggerStay (Collider other) {
		if (other.tag == "Enemy") {
			other.GetComponent<EnemyBehavior> ().isBrambleSlowed = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Enemy") {
			other.GetComponent<EnemyBehavior> ().isBrambleSlowed = false;
		}
	}
}
