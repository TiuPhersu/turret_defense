using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour {
    [SerializeField] Collider collisionMesh;
    [SerializeField] int hitPoints = 10;
    [SerializeField] ParticleSystem hitParticlePrefab;
    [SerializeField] ParticleSystem deathParticlePrefab;
    [SerializeField] AudioClip enemyHitSFX;
    [SerializeField] AudioClip enemyDeathSFX;



    // Use this for initialization
    void Start () {
		
	}
    private void OnParticleCollision(GameObject other){
        //print("I'm hit");
        ProcessHit();
        if (hitPoints < 1) {
            KillEnemy();
        }
    }

    void ProcessHit() {
        GetComponent<AudioSource>().PlayOneShot(enemyHitSFX);
        hitPoints = hitPoints - 1;
        hitParticlePrefab.Play();
        //print("current hitpoints are " + hitPoints);
    }

    private void KillEnemy(){
        // important to instantiate before destroying this object
        var vfx = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        vfx.Play();
        float destroyDelay = vfx.main.duration;//get the duration of the death vfx
        Destroy(vfx.gameObject, destroyDelay);//delay the destruction of the object
        AudioSource.PlayClipAtPoint(enemyDeathSFX, Camera.main.transform.position);// play death sfx after object is destroyed has to be heard from the camera position
        Destroy(gameObject);// destroy the enemy
    }
}
