using UnityEngine;
using System.Collections;

public class Placer : MonoBehaviour {

	public GameObject goodChild;
	public GameObject badChild;

	public bool goodPlace = true;
	public bool rendInChildren = false;

	void Start () {
		if (rendInChildren) {
			Renderer[] rends;
			rends = goodChild.GetComponentsInChildren<Renderer> ();

			foreach (Renderer rend in rends) {
				rend.enabled = true;
			}

			rends = badChild.GetComponentsInChildren<Renderer> ();
			foreach (Renderer rend in rends) {
				rend.enabled = false;
			}

		} else { 
			goodChild.GetComponent<Renderer> ().enabled = true;
			badChild.GetComponent<Renderer> ().enabled = false;
		}
	}

	void OnTriggerEnter (Collider other) {
		if (goodPlace && other.tag == "Construction") {
			goodPlace = false;

			if (rendInChildren) {
				Renderer[] rends;
				rends = goodChild.GetComponentsInChildren<Renderer> ();

				foreach (Renderer rend in rends) {
					rend.enabled = false;
				}

				rends = badChild.GetComponentsInChildren<Renderer> ();
				foreach (Renderer rend in rends) {
					rend.enabled = true;
				}

			} else { 
				goodChild.GetComponent<Renderer> ().enabled = false;
				badChild.GetComponent<Renderer> ().enabled = true;
			}
		}
	}

	void OnTriggerExit (Collider other) {
		if (!goodPlace && other.tag == "Construction") {
			goodPlace = true;

			if (rendInChildren) {
				Renderer[] rends;
				rends = goodChild.GetComponentsInChildren<Renderer> ();

				foreach (Renderer rend in rends) {
					rend.enabled = true;
				}

				rends = badChild.GetComponentsInChildren<Renderer> ();
				foreach (Renderer rend in rends) {
					rend.enabled = false;
				}

			} else { 
				goodChild.GetComponent<Renderer> ().enabled = true;
				badChild.GetComponent<Renderer> ().enabled = false;
			}
		}
	}
}
