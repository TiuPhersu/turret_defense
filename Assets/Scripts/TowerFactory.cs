using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour {
    [SerializeField] int towerLimit = 5;
    [SerializeField] Tower towerPrefab;

    public void AddTower(Waypoint baseWaypoint){
        var towers = FindObjectsOfType<Tower>();
        int numTowers = towers.Length;
        if (numTowers != towerLimit){
            InstantiateNewTower(baseWaypoint);
        }
        else { MoveExistingTower(); }
    }

    private void InstantiateNewTower(Waypoint baseWaypoint)
    {
        Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity);
        baseWaypoint.isPlaceable = false;
    }

    private static void MoveExistingTower()
    {
        print("Too Many towers");
    }
}
