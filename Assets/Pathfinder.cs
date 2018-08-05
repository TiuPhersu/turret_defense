using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {
    [SerializeField] Waypoint startWaypoint, endWaypoint;
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    [SerializeField] bool isRunning = true;// todo make private
    Vector2Int[] directions = { Vector2Int.up,
                                Vector2Int.right,
                                Vector2Int.down,
                                Vector2Int.left};

    // Use this for initialization
	void Start () {
        LoadBlocks();
        ColorStartAndEnd();
        //ExploreNeighbours();
        Pathfind();
	}

    private void Pathfind()
    {
        queue.Enqueue(startWaypoint);//place the first node in the queue
        while (queue.Count > 0 && isRunning)//check if there are still items in the node and if isRunning is true
        {
            var searchCenter = queue.Dequeue();//pull the first item in the queue (basically a line of people)
            searchCenter.isExplored = true;
            print("Searching from: " + searchCenter);//logs the value
            HaltIfEndFound(searchCenter);//end search if found
            ExploreNeighbours(searchCenter);
        }
        print("Finished pathfinding");
    }

    private void HaltIfEndFound(Waypoint searchCenter)
    {//end the search
        if (searchCenter == endWaypoint) {
            print("Searching from end node, ending");// todo remove log
            isRunning = false;
        }
    }

    private void ExploreNeighbours(Waypoint from)
    {//explore the neighbors of the nodes
        if (!isRunning) { return; }//end function if the isRunning is false
        foreach (Vector2Int direction in directions) {
            Vector2Int neighborCoordinates = direction + from.GetGridPos(); 
            //print("Exploring " + neighborCoordinates);
            try
            {
                QueueNewNeighbours(neighborCoordinates);
            }
            catch { //do nothing
            }
        }
    }

    private void QueueNewNeighbours(Vector2Int neighborCoordinates)
    {
        Waypoint neighbour = grid[neighborCoordinates];
        if (neighbour.isExplored || queue.Contains(neighbour)){
            //do nothing
        }
        else {
            neighbour.SetTopColor(Color.blue);// todo move later
            queue.Enqueue(neighbour);
            print("Queueing " + neighbour);
        }
    }

    private void ColorStartAndEnd(){
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints){
            waypoint.SetTopColor(Color.gray);//change all top to grey
        }

        startWaypoint.SetTopColor(Color.green);//set start as green
        endWaypoint.SetTopColor(Color.red);//set end as red
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
