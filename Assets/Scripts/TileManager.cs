using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

    public int width = 6;
    public int height = 3;

    public GameObject tile_prefab;
    GameObject[,] tileArray;

	// Use this for initialization
	void Start () {
        tileArray = new GameObject[width, height];
        InitializeBoard();
	}

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
}
