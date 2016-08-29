using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	public GameObject enemyPrefab;
	public GameObject[] spawnNodes;

	public int numEnemies = 25;
	public int waveStrength = 10;

	public float initialDelay = 15f;
	public float spawnInterval = 5f;

	// Use this for initialization
	void Start () {
		StartCoroutine (manageSpawn ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator manageSpawn () {

		yield return new WaitForSeconds (initialDelay);

		while (true) {	// While Wave is active etc.

			GameObject[] activeEnemies = GameObject.FindGameObjectsWithTag ("Enemy");

			if (activeEnemies.Length <= numEnemies) {
				int randIndex = Random.Range (0, spawnNodes.Length);

				for (int i = 0; i < this.waveStrength; i++) {
					GameObject newEnemy = GameObject.Instantiate (enemyPrefab);

					//Pick a spawn node "at random"

					newEnemy.transform.position = spawnNodes [randIndex].transform.position;
					newEnemy.GetComponent<EnemyBehavior> ().prevNode = spawnNodes [randIndex];

					yield return new WaitForSeconds (Random.Range (0.2f, 1f));
				}
			}

			yield return new WaitForSeconds (spawnInterval);
		}
	}
}
