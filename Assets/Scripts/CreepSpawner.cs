using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepSpawner : MonoBehaviour {

	public Transform creepPrefab;
	public GameObject Path;
	public float timeBetweenWaves = 20f;
	public float countdown = 2f;
	public Transform spawnPoint;

	private int waveNumber = 1;

	void Update(){
		if (countdown <= 0) {
			StartCoroutine(SpawnWave());
			countdown = timeBetweenWaves;
		}
		countdown -= Time.deltaTime;
	}

	IEnumerator SpawnWave() {

		for (int i = 0; i < 3; i++) {
			SpawnCreep ();
			yield return new WaitForSeconds (0.4f);
		}

		waveNumber++;
	}
	void SpawnCreep(){
		Transform creep = Instantiate (creepPrefab, spawnPoint.position, spawnPoint.rotation) as Transform;
		CreepMovement script = creep.GetComponent<CreepMovement>();
		script.Path = Path as GameObject;
		
	}
}
