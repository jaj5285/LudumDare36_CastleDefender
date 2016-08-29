using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	public bool GameRunning = true;

	public GameObject enemyPrefab;
	public GameObject bossPrefab;

	public GameObject[] spawnNodes;

	public int waveNum = 0;

	public int levelRadix = 5;
	public int numLevels = 4;

	public int numEnemies = 25;
	public int waveStrength = 3;

	public float initialDelay = 15f;
	public float waveSpawnInterval = 5f;

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

			// Waits for clear board after end of level
			if (this.waveNum % this.levelRadix == 0) {
				GameObject[] activeEnemies = GameObject.FindGameObjectsWithTag ("Enemy");

				if (activeEnemies.Length == 0) {
					StartCoroutine (spawnWave ());
				}
			} else {
				StartCoroutine (spawnWave ());
			}

			yield return new WaitForSeconds (waveSpawnInterval);
		}
	}

	IEnumerator spawnWave () {
		this.waveNum++;

		int waveModRadix = this.waveNum % this.levelRadix;
		int quotRadix = this.waveNum / this.levelRadix;


	}
}
