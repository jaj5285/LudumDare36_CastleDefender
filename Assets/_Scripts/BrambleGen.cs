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
		for (int i = 0; i < vineCount; i++) {
			newVines [i] = GameObject.Instantiate (vinePrefab);
			newVines [i].transform.SetParent (this.transform);
			newVines [i].transform.localScale = new Vector3 (Random.Range (0.3f, 0.5f), Random.Range (0.3f, 0.5f), Random.Range (0.3f, 0.5f));
			Vector2 randPos = radius * Random.insideUnitCircle;
			newVines [i].transform.localPosition = new Vector3(randPos.x, this.verticalOffset, randPos.y);
			newVines [i].transform.rotation = Quaternion.Euler (270, Random.Range (0f, 360f), 0f);
		}

		float startTime = Time.time;

		while ((Time.time - startTime) < this.genTime) {
			for (int i = 0; i < vineCount; i++) {
				if (Random.Range (0f, 1f) <= this.growthProbability) {
					float curGrowth = Mathf.Clamp (newVines [i].transform.localPosition.y + this.growthAmount, this.verticalOffset, 0f);
					newVines [i].transform.localPosition = new Vector3 (newVines[i].transform.localPosition.x, curGrowth, newVines[i].transform.localPosition.z);
				}
			}
			yield return new WaitForEndOfFrame ();
		}
	}
}
