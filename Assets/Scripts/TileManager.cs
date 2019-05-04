using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

    int width = 6;
    int height = 3;

    public GameObject tile_prefab;
    GameObject[,] tileArray;

    private void Awake() {
        tileArray = new GameObject[width, height];        
        InitializeBoard();
    }

    // Use this for initialization
    void Start () {
	}

    //makes a new Tile gameobject for each spot in the 2D array
    private void InitializeBoard() {
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                tileArray[i,j] = Instantiate(tile_prefab, new Vector3(i, 0, j), Quaternion.identity);
                //Debug.Log("Making a new tile at " + i + ", " + j);
                tileArray[i,j].GetComponent<Tile>().xCoord = i;
                tileArray[i, j].GetComponent<Tile>().yCoord = j;
                SetTileNeighbors(tileArray[i, j]);
                //tileArray[i, j].GetComponent<Tile>().ListNeighbors();
            }
        }
    }

    //sets each Tile to know whether it has neighbors on any given side
    private void SetTileNeighbors(GameObject tile) {
        Tile tile_script = tile.GetComponent<Tile>();
        int currentX = tile_script.xCoord;
        int currentY = tile_script.yCoord;

        //check left side of board
        if (currentX > 0) {
            tile_script.HasWestNeighbor = true;
        }
        else {
            tile_script.HasWestNeighbor = false;
        }

        //check right side of board
        if(currentX < width-1) {
            tile_script.HasEastNeighbor = true;
        }
        else {
            tile_script.HasEastNeighbor = false;
        }

        //check top of board
        if (currentY < height - 1) {
            tile_script.HasNorthNeighbor = true;
        }
        else {
            tile_script.HasNorthNeighbor = false;
        }

        //check bottom of board
        if(currentY > 0) {
            tile_script.HasSouthNeighbor = true;
        }
        else {
            tile_script.HasSouthNeighbor = false;
        }

    }

    public GameObject GetTileAtCoords(int x, int y) {
        if((x >= 0 && x < width) && (y >= 0 && y < height)) {
            return tileArray[x, y];
        }
        else {
            return null;
        }
    }

    //sets the Current Occupant of Tile at coords x,y to be the passed in GameObject
    public bool PutOccupantAtCoords(GameObject occupant, int x, int y) {
        if(occupant == null) {
            Debug.LogError("Null Occupant attempted to be placed.");
        }

        //check for valid coords and non-null occupant and then do operation
        if (occupant != null && (x >= 0 && x < width) && (y >= 0 && y < height)) {
            tileArray[x, y].GetComponent<Tile>().CurrentOccupant = occupant;
            return true;
        }
        else {
            return false;
        }
    }
    
    //get Current Occupant of Tile at coords x,y
    public GameObject GetOccupantAtCoords(int x, int y) {
        //check for valid coords and then do operation
        if ((x >= 0 && x < width) && (y >= 0 && y < height)) {
            return tileArray[x, y].GetComponent<Tile>().CurrentOccupant;
        }
        else {
            return null;
        }
    }

    public bool RemoveOccupantAtCoords(int x, int y) {
        //check for valid coords and then do operation
        if ((x >= 0 && x < width) && (y >= 0 && y < height)) {
            tileArray[x, y].GetComponent<Tile>().CurrentOccupant = null;
            Debug.Log("occupant at " + x + ", " + y + " has been removed");
            return true;
        }
        else {
            return false;
        }
    }

    public int Width {
        get { return width; }
    }

    public int Height {
        get { return width; }
    }
}
