using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    //Parameters of each tower
    [SerializeField] Transform objectToPan;
    [SerializeField] float attackRange = 10f;
    [SerializeField] ParticleSystem projectileParticle;

    //State of each tower
    Transform targetEnemy;
	// Update is called once per frame
	void Update () {
        SetTargetEnemy();
        if (targetEnemy){
            objectToPan.LookAt(targetEnemy);// look at enemy;
            FireAtEnemy();//determin the distance
        } else {
            Shoot(false);
        }
	}

    private void SetTargetEnemy(){
        // Get the collection of things
        var sceneEnemies = FindObjectsOfType<EnemyDamage>();
        if (sceneEnemies.Length == 0) { return; }
        //Assume the first is the "winner"
        Transform closetEnemy = sceneEnemies[0].transform;
        // for each item in collection
        foreach (EnemyDamage testEnemy in sceneEnemies) {
            // update winner
            closetEnemy = GetCloset(closetEnemy, testEnemy.transform);
        }
        // return the winner
        targetEnemy = closetEnemy;
    }

    private Transform GetCloset(Transform transformA , Transform transformB){
        var distToA = Vector3.Distance(transform.position, transformA.position);
        var distToB = Vector3.Distance(transform.position, transformA.position);

        if (distToA < distToB) {
            return transformA;
        }
        return transformB;
    }

    private void FireAtEnemy() {
        float distanceToEnemy = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);
        if (distanceToEnemy <= attackRange) {
            Shoot(true);
        } else {
            Shoot(false);
        }
    }
    private void Shoot(bool isActive) {
        var emissionModule = projectileParticle.emission;
        emissionModule.enabled = isActive;
    }
}
