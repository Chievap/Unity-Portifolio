using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour {
    public int curWave;
    public int startEnemyNumber;
    public float waveMultiPlier;
    public float spawnTimer;
    public int enemysToSpawn;
    public int nextWaveTimer;

    public GameObject enemyPrefab;
    public GameObject endPoint;
    public GameObject grid;

    public Transform spawnPoint;

    public bool waveDone = false;



	public void StartWave() {
        enemysToSpawn = CalculateNewEnemys();
        grid.GetComponent<Grid>().CreateGrid();
        StartCoroutine("SpawnEnemys");
    }
	
	void Update () {
        if (waveDone) {
            StartCoroutine("WaveCountDown");
        }
	}
    public IEnumerator WaveCountDown() {
        yield return new WaitForSeconds(nextWaveTimer);
        waveDone = false;
        StartCoroutine("SpawnEnemys");
        StopCoroutine("WaveCountDown");
    }

    public IEnumerator SpawnEnemys() {

        yield return new WaitForSeconds(spawnTimer);
        if(enemysToSpawn <= 0) {
            curWave++;
            enemysToSpawn = CalculateNewEnemys();
            print("Calculating enemy number");
            waveDone = true;

        }else {
            GameObject tempEnemy;
            enemysToSpawn--;
            print("Spawn");
            tempEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity) as GameObject;
            tempEnemy.GetComponent<NavMeshAgent>().SetDestination(endPoint.transform.position);
            StartCoroutine("SpawnEnemys");
        }

    }

    public int CalculateNewEnemys() {
        int enemys;
        enemys = Mathf.RoundToInt((startEnemyNumber + curWave) * waveMultiPlier);
        curWave++;
        return enemys;
    }
}
