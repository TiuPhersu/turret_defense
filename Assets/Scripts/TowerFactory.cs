using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour {
    [SerializeField] int towerLimit = 5;
    [SerializeField] Tower towerPrefab;
    [SerializeField] Transform towerParentTransform;

    // create an empty queue of towers
    Queue<Tower> towerQueue = new Queue<Tower>();//initialize queue

    public void AddTower(Waypoint baseWaypoint){
        int numTowers = towerQueue.Count;
        print(numTowers);
        if (numTowers != towerLimit){
            InstantiateNewTower(baseWaypoint);
        } else { MoveExistingTower(baseWaypoint); }
    }

    private void InstantiateNewTower(Waypoint baseWaypoint)
    {
        var newTower = Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity);
        newTower.transform.parent = towerParentTransform;
        baseWaypoint.isPlaceable = false;

        // set the baseWaypoints
        newTower.baseWaypoint = baseWaypoint;
        baseWaypoint.isPlaceable = false;

        // put new tower into queue
        towerQueue.Enqueue(newTower);
    }

    private void MoveExistingTower(Waypoint newBaseWaypoint)
    {
        var oldTower = towerQueue.Dequeue();// take bottom tower off queue

        // set the placeable flags
        oldTower.baseWaypoint.isPlaceable = true;
        newBaseWaypoint.isPlaceable = false;

        // set the baseWaypoints
        oldTower.baseWaypoint = newBaseWaypoint;

        oldTower.transform.position = newBaseWaypoint.transform.position;

        // put the old tower on top of the queue
        towerQueue.Enqueue(oldTower);

        print("Too Many towers");
    }
}
