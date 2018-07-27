using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public TileManager tileManager;
    public int numOfRowsToLob = 3;

	// Use this for initialization
	void Start () {
        tileManager = GameObject.Find("Tile Manager").GetComponent<TileManager>();
	}
	
	// Update is called once per frame
	void Update () {
        MovePlayer();
        HandleInputs();
	}

    private void HandleInputs() {

        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            //Debug.Log("Finding space player is on with inputs: " + transform.position.x + ", " + transform.position.z);
            //tileManager.GetTileAtCoords((int)transform.position.x, (int)transform.position.z).GetComponent<Tile>().ListCoords();
            ArrayList tilesToHit = GetTilesInColumn();

            StartCoroutine(AttackInGrouping(tilesToHit));
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            ArrayList tilesToHit = GetTilesInRow(numOfRowsToLob); //GetTilesInColumn();

            StartCoroutine(AttackInGrouping(tilesToHit));
        }
    }

    private ArrayList GetTilesInColumn() {
        ArrayList tilesToHit = new ArrayList();

        tilesToHit.Add(tileManager.GetTileAtCoords((int)transform.position.x + 1, (int)transform.position.z));
        tilesToHit.Add(tileManager.GetTileAtCoords((int)transform.position.x + 2, (int)transform.position.z));
        tilesToHit.Add(tileManager.GetTileAtCoords((int)transform.position.x + 3, (int)transform.position.z));

        return tilesToHit;
    }

    private ArrayList GetTilesInRow(int numSpacesFromPlayerToRow) {
        ArrayList tilesToHit = new ArrayList();

        tilesToHit.Add(tileManager.GetTileAtCoords((int)transform.position.x + numSpacesFromPlayerToRow, (int)transform.position.z -1));
        tilesToHit.Add(tileManager.GetTileAtCoords((int)transform.position.x + numSpacesFromPlayerToRow, (int)transform.position.z));
        tilesToHit.Add(tileManager.GetTileAtCoords((int)transform.position.x + numSpacesFromPlayerToRow, (int)transform.position.z + 1));

        return tilesToHit;
    }

    IEnumerator AttackInGrouping(ArrayList tilesToHit) {
        ChangeTileColors(tilesToHit, "attack");
        yield return new WaitForSeconds(.5f);
        ChangeTileColors(tilesToHit, "");
    }

    private void ChangeTileColors(ArrayList tilesToHit, String type) {
        Color color;
        if (type.Equals("attack")) {
            color = Color.red;
        }
        else {
            color = Color.white;
        }

        foreach (GameObject tile in tilesToHit) {
            if (tile != null) {
                Renderer rend = tile.GetComponent<Renderer>();
                rend.material.SetColor("_Color", color);
            }
        }
    }

    void MovePlayer() {
        float currentX = this.transform.position.x;
        float currentZ = this.transform.position.z;

        if(Input.GetKeyDown(KeyCode.A) && currentX > 0) {
            currentX--;
        }

        if(Input.GetKeyDown(KeyCode.D) && currentX < 2){
            currentX++;
        }

        if(Input.GetKeyDown(KeyCode.W) && currentZ < 2) {
            currentZ++;
        }
        
        if(Input.GetKeyDown(KeyCode.S) && currentZ > 0) {
            currentZ--;
        }

        this.transform.position = new Vector3(currentX, 0, currentZ);
    }
}
