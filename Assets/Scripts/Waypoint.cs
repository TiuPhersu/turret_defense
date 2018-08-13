﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {
    //encapsulation
    //public ok here as this is a data class
    public bool isExplored = false;// ok as is a data class public
    public Waypoint exploredFrom;

    Vector2Int gridPos;
    const int gridSize = 10;

    public int GetGridSize() {
        return gridSize;
    }

    public Vector2Int GetGridPos() {
        return new Vector2Int (
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize)
        );
    }

    public void SetTopColor(Color color) {
        MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
        topMeshRenderer.material.color = color;  
    }
}