using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] float secondsBetweenSpawns = 10f;
    [SerializeField] EnemyMovement enemyPrefab;
	// Use this for initialization
	void Start () {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn(){
        while (true){
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            //print("Spawning");
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }
}
