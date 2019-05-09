using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTile : MonoBehaviour {

    public bool HasNorthNeighbor { get; set; }
    public bool HasWestNeighbor { get; set; }
    public bool HasSouthNeighbor { get; set; }
    public bool HasEastNeighbor { get; set; }

    public int xCoord { get; set; }
    public int yCoord { get; set; }

    public GameObject CurrentOccupant;
    private GameObject thisTileGraphic;

    private void Awake()
    {
        CurrentOccupant = null;
    }

    // Use this for initialization
    void Start()
    {

    }

    public void ListNeighbors()
    {
        Debug.Log("Tile at " + xCoord + ", " + yCoord + " Neighbor North? " + HasNorthNeighbor + " Neighbor East? " + HasEastNeighbor + " Neighbor South? " + HasSouthNeighbor + " Neighbor West? " + HasWestNeighbor);
    }

    public void ListCoords()
    {
        Debug.Log("Tile at " + xCoord + ", " + yCoord);
    }

    public void SetTileGameObject(GameObject go, Vector3 spawnPosition)
    {
        thisTileGraphic = go;
        Instantiate(thisTileGraphic, spawnPosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}