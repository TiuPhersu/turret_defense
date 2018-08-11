using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
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
            yield return new WaitForSeconds(2f);
        }
        print("Ending patrol...");
    }
    
	// Update is called once per frame
	void Update () {
		
	}
}
