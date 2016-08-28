using UnityEngine;
using System.Collections;

public class FlameStatue : MonoBehaviour {

	public GameObject flamePrefab;

	public float fireDuration = 2f;
	public float fireCoolDown = 0.5f;

	public bool isFireActive = false;

	void OnTriggerStay (Collider other) {
		if (other.gameObject.tag == "Enemy") {
			if (!this.isFireActive) {
				StartCoroutine (doFire ());
			}
		}
	}

	IEnumerator doFire () {
		this.isFireActive = true;

		GameObject flame = GameObject.Instantiate (flamePrefab);

		flame.transform.SetParent (this.transform);
		flame.transform.localPosition = Vector3.zero;
		flame.transform.localRotation = Quaternion.identity;

		yield return new WaitForEndOfFrame ();

		flame.GetComponent<Flamethrower> ().Activate ();

		yield return new WaitForSeconds (fireDuration);

		Destroy (flame);

		yield return new WaitForSeconds (fireCoolDown);

		this.isFireActive = false;
	}
}
