using UnityEngine;
using System.Collections;

public class Placer : MonoBehaviour {

	public GameObject goodChild;
	public GameObject badChild;

	public bool goodPlace = true;

	void Start () {
		goodChild.GetComponent<Renderer> ().enabled = true;
		badChild.GetComponent<Renderer> ().enabled = false;
	}

	void OnTriggerEnter (Collider other) {
		if (goodPlace && other.tag == "Construction") {
			goodPlace = false;

			goodChild.GetComponent<Renderer> ().enabled = false;
			badChild.GetComponent<Renderer> ().enabled = true;
		}
	}

	void OnTriggerExit (Collider other) {
		if (!goodPlace && other.tag == "Construction") {
			goodPlace = true;

			goodChild.GetComponent<Renderer> ().enabled = true;
			badChild.GetComponent<Renderer> ().enabled = false;
		}
	}
}
