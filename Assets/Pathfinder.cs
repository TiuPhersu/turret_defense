using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>(); 
	// Use this for initialization
	void Start () {
        LoadBlocks();
	}

    private void LoadBlocks(){
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints) {
            bool isOverlapping = grid.ContainsKey(waypoint.GetGridPos());
            if (isOverlapping)
            {
                Debug.LogWarning("Skipping overlapping block " + waypoint);
            }
            else {
                grid.Add(waypoint.GetGridPos(), waypoint);
            }
        }
        print("Loaded " + grid.Count + " blocks");
    }
}
