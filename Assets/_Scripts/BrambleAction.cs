using UnityEngine;
using System.Collections;

public class BrambleAction : MonoBehaviour {

	public float brambleMoveSpeed = 2.5f;
	public float standardMoveSpeed = 5f;

	void OnTriggerStay (Collider other) {
		if (other.tag == "Enemy") {
			other.GetComponent<EnemyBehavior> ().travelSpeed = brambleMoveSpeed;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Enemy") {
			other.GetComponent<EnemyBehavior> ().travelSpeed = standardMoveSpeed;
		}
	}
}
