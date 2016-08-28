using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	public GameObject enemyPrefab;
	public GameObject[] spawnNodes;

	public int numEnemies = 10;

	public float spawnInterval = 5f;

	// Use this for initialization
	void Start () {
		StartCoroutine (manageSpawn ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator manageSpawn () {
		while (true) {	// While Wave is active etc.
			GameObject newEnemy = GameObject.Instantiate(enemyPrefab);

			//Pick a spawn node "at random"
			int randIndex = Random.Range(0, spawnNodes.Length);
			newEnemy.transform.position = spawnNodes [randIndex].transform.position;
			newEnemy.GetComponent<EnemyBehavior> ().prevNode = spawnNodes [randIndex];

			yield return new WaitForSeconds (spawnInterval);
		}
	}
}
