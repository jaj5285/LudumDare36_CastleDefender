using UnityEngine;
using System.Collections;

public class BrambleGen : MonoBehaviour {

	public float genTime = 2f;
	public float genInterval = 0.2f;

	public float growthProbability = 0.4f;
	public float growthAmount = 0.1f;

	public float radius = 10f;
	public int vineCount = 25;

	public float verticalOffset;

	public GameObject vinePrefab;

	// Use this for initialization
	void Start () {
		StartCoroutine (HandleVineGen ());
	}

	IEnumerator HandleVineGen () {
		GameObject[] newVines = new GameObject[vineCount];
		Vector3[] startPos = new Vector3[vineCount];
		for (int i = 0; i < vineCount; i++) {
			newVines [i] = GameObject.Instantiate (vinePrefab);
			newVines [i].transform.SetParent (this.transform);
			newVines [i].transform.localScale = new Vector3 (Random.Range (0.3f, 0.5f), Random.Range (0.3f, 0.5f), Random.Range (0.3f, 0.5f));
			Vector2 randPos = radius * Random.insideUnitCircle;
			Vector3 idealPos = new Vector3 (randPos.x, 0f, randPos.y) + this.transform.position;

			RaycastHit groundHit;
			if (Physics.Raycast (idealPos, -Vector3.up, out groundHit)) {
				idealPos -= new Vector3 (0, groundHit.distance, 0);
			}

			startPos [i] = idealPos - new Vector3 (0f, this.verticalOffset, 0f);
			newVines [i].transform.position = startPos[i];
			newVines [i].transform.rotation = Quaternion.Euler (270, Random.Range (0f, 360f), 0f);
		}

		float startTime = Time.time;

		while ((Time.time - startTime) < this.genTime) {
			for (int i = 0; i < vineCount; i++) {
				if (Random.Range (0f, 1f) <= this.growthProbability) {
					float curGrowth = newVines [i].transform.position.y - startPos [i].y;
					curGrowth = Mathf.Clamp (curGrowth + growthAmount, 0f, this.verticalOffset);
					newVines [i].transform.position = new Vector3 (startPos [i].x, startPos[i].y + curGrowth, startPos [i].z); 
				}
			}
			yield return new WaitForEndOfFrame ();
		}
	}
}
