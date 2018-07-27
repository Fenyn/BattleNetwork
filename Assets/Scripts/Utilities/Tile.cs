using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public bool HasNorthNeighbor { get; set; }
    public bool HasWestNeighbor { get; set; }
    public bool HasSouthNeighbor { get; set; }
    public bool HasEastNeighbor { get; set; }

    public int xCoord { get; set; }
    public int yCoord { get; set; }

    public GameObject CurrentOccupant { get; protected set; }


    // Use this for initialization
    void Start () {
	}

    public void ListNeighbors() {
        Debug.Log("Tile at " + xCoord + ", " + yCoord + " Neighbor North? " + HasNorthNeighbor + " Neighbor East? " + HasEastNeighbor + " Neighbor South? " + HasSouthNeighbor + " Neighbor West? " + HasWestNeighbor);
    }

    public void ListCoords() {
        Debug.Log("Tile at " + xCoord + ", " + yCoord);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
