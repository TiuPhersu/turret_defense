using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {
    [SerializeField] Waypoint startWaypoint, endWaypoint;//set the end waypoint and start waypoing
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();//initialize the dictionary data structure where it finds a word(key) and has a definition(value)
    Queue<Waypoint> queue = new Queue<Waypoint>();//initialize queue
    bool isRunning = true;//set if running or not
    Waypoint searchCenter; //the current search center
    List<Waypoint> path = new List<Waypoint>(); 

    Vector2Int[] directions = { Vector2Int.up,
                                Vector2Int.right,
                                Vector2Int.down,
                                Vector2Int.left};

    public List<Waypoint> GetPath() {
        if (path.Count == 0){// check if the path is already moved
            CalculatePath();//calculate path
        }
        return path;

    }

    private void CalculatePath()
    {
        LoadBlocks();
//        ColorStartAndEnd();
        BreadthFirstSearch();
        CreatePath();
    }

    private void CreatePath()
    {
        SetAsPath(endWaypoint);
        
        Waypoint previous = endWaypoint.exploredFrom;
        while (previous != startWaypoint) {
            // add intermediate waypoints
            SetAsPath(previous);
            previous = previous.exploredFrom;
        }
        SetAsPath(startWaypoint);
        path.Reverse();//reverse the list

    }

    private void SetAsPath(Waypoint waypoint) {
        path.Add(waypoint);
        waypoint.isPlaceable = false;
    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(startWaypoint);//place the first node in the queue
        while (queue.Count > 0 && isRunning)//check if there are still items in the node and if isRunning is true
        {
            searchCenter = queue.Dequeue();//pull the first item in the queue (basically a line of people)
            searchCenter.isExplored = true;
            //print("Searching from: " + searchCenter);//logs the value
            HaltIfEndFound();//end search if found
            ExploreNeighbours();
        }
    }

    private void HaltIfEndFound()
    {//end the search
        if (searchCenter == endWaypoint) {
            //print("Searching from end node, ending");// todo remove log
            isRunning = false;
        }
    }

    private void ExploreNeighbours()
    {//explore the neighbors of the nodes
        if (!isRunning) { return; }//end function if the isRunning is false
        foreach (Vector2Int direction in directions) {
            Vector2Int neighborCoordinates = direction + searchCenter.GetGridPos();
            //print("Exploring " + neighborCoordinates);
            if (grid.ContainsKey(neighborCoordinates)) { 
                QueueNewNeighbours(neighborCoordinates);
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
            //neighbour.SetTopColor(Color.blue);// todo move later
            queue.Enqueue(neighbour);
            neighbour.exploredFrom = searchCenter;
            //print("Queueing " + neighbour);
        }
    }

 //   private void ColorStartAndEnd(){
 //       //todo consider moving out
 //       var waypoints = FindObjectsOfType<Waypoint>();
 //       foreach (Waypoint waypoint in waypoints){
 //           waypoint.SetTopColor(Color.gray);//change all top to grey
 //       }
 //       startWaypoint.SetTopColor(Color.green);//set start as green
 //       endWaypoint.SetTopColor(Color.red);//set end as red
 //   }

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
        //print("Loaded " + grid.Count + " blocks");
    }
}
