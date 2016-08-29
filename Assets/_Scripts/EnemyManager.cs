using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	public bool GameRunning = true;
	public bool waveActive = false;

	public GameObject weakEnemyPrefab;
	public GameObject enemyPrefab;
	public GameObject bossPrefab;

	public GameObject[] spawnNodes;

	public int waveNum = 0;

	public int levelRadix = 5;
	public int numLevels = 4;

	public int waveStrength = 4;

	public float initialDelay = 15f;
	public float waveSpawnInterval = 45f;
	public float waveCheckInterval = 10f;

	public int enemyCost = 4;
	public int weakEnemyCost = 3;

	// Use this for initialization
	void Start () {
		StartCoroutine (manageSpawn ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator manageSpawn () {

		yield return new WaitForSeconds (initialDelay);

		while (GameRunning) {	// While Wave is active etc.

			if (!waveActive) {
				StartCoroutine (spawnWave ());
			}

			GameObject[] activeEnemies = GameObject.FindGameObjectsWithTag ("Enemy");
			if (activeEnemies.Length == 0) {
				StartCoroutine (spawnWave ());
			}

			yield return new WaitForSeconds (waveSpawnInterval);
		}
	}

	IEnumerator spawnWave () {
		this.waveActive = true;
		this.waveNum++;

		int waveModRadix = this.waveNum % this.levelRadix;
		int quotRadix = this.waveNum / this.levelRadix;

		// Boss Wave
		if (waveModRadix == 0) {
			int numBosses = (quotRadix > (this.numLevels / 2)) ? (2) : (1);

			int squadStrength = waveStrength * (quotRadix + 1);

			for (int i = 0; i < numBosses; i++) {
				int randIndex = Random.Range (0, spawnNodes.Length);

				GameObject newEnemy = GameObject.Instantiate (bossPrefab);
				newEnemy.transform.position = this.spawnNodes [randIndex].transform.position;
				bossPrefab.GetComponent<EnemyBehavior> ().prevNode = this.spawnNodes [randIndex];

				while (squadStrength > 0) {
					if (Random.Range (0f, 1f) > 0.5f) {
						newEnemy = GameObject.Instantiate (enemyPrefab);
						newEnemy.transform.position = this.spawnNodes [randIndex].transform.position;
						newEnemy.GetComponent<EnemyBehavior> ().prevNode = this.spawnNodes [randIndex];

						squadStrength -= enemyCost;
					} else {
						newEnemy = GameObject.Instantiate (weakEnemyPrefab);
						newEnemy.transform.position = this.spawnNodes [randIndex].transform.position;
						newEnemy.GetComponent<EnemyBehavior> ().prevNode = this.spawnNodes [randIndex];

						squadStrength -= weakEnemyCost;
					}

					yield return new WaitForSeconds (Random.Range (0.3f, 1.1f));
				}
			}

		} else {	// Normal Wave

			int numSquads = waveModRadix + 1;
			int squadStrength = waveModRadix * waveStrength * (quotRadix + 1);

			for (int i = 0; i < numSquads; i++) {
				int randIndex = Random.Range (0, spawnNodes.Length);

				while (squadStrength > 0) {
					if (Random.Range (0f, 1f) > 0.5f) {
						GameObject newEnemy = GameObject.Instantiate (enemyPrefab);
						newEnemy.transform.position = this.spawnNodes [randIndex].transform.position;
						newEnemy.GetComponent<EnemyBehavior> ().prevNode = spawnNodes [randIndex];

						squadStrength -= enemyCost;
					} else {
						GameObject newEnemy = GameObject.Instantiate (weakEnemyPrefab);
						newEnemy.transform.position = this.spawnNodes [randIndex].transform.position;
						newEnemy.GetComponent<EnemyBehavior> ().prevNode = spawnNodes [randIndex];

						squadStrength -= weakEnemyCost;
					}

					yield return new WaitForSeconds (Random.Range (0.3f, 1.1f));
				}
			}
		}

		yield return new WaitForSeconds (waveSpawnInterval);
	}
}
