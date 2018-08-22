using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] float secondsBetweenSpawns = 10f;
    [SerializeField] EnemyMovement enemyPrefab;
    [SerializeField] Text scoreText;
    [SerializeField] Transform enemyParentTransform;
    [SerializeField] AudioClip spawnedEnemySFX;

    int score = 0;

    // Use this for initialization
    void Start () {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn(){
        while (true)
        {
            AddScore();
            GetComponent<AudioSource>().PlayOneShot(spawnedEnemySFX);
            var newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);//spawn enemy
            newEnemy.transform.parent = enemyParentTransform;//place enemy into the object enemies(more organized)
            //print("Spawning");

            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }

    private void AddScore()
    {
        score++;
        scoreText.text = score.ToString();//display score
    }
}
