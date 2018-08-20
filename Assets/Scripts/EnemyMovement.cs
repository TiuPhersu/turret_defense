using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] float movementPeriod = .5f;
    [SerializeField] ParticleSystem goalParticle;


    // Use this for initialization
    void Start (){
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        var path = pathfinder.GetPath();
        StartCoroutine(FollowPath(path));
    }

    IEnumerator FollowPath(List<Waypoint> path){
        print("Starting patrol...");
        foreach (Waypoint waypoint in path){
            //print(waypoint.name);
            transform.position = waypoint.transform.position;// enemy moves
            //print("Visiting blocks " + waypoint.name);
            yield return new WaitForSeconds(movementPeriod);
        }
        SelfDestruct();
        print("Ending patrol...");
    }

    private void SelfDestruct(){
        // important to instantiate before destroying this object
        var vfx = Instantiate(goalParticle, transform.position, Quaternion.identity);
        vfx.Play();
        float destroyDelay = vfx.main.duration;//get the duration of the death vfx
        Destroy(vfx.gameObject, destroyDelay);
        Destroy(gameObject);// destroy the enemy
    }
}
