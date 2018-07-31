using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCoroutines : MonoBehaviour {

    TileManager tileManager;
    PlayerController player;
    int damage;

    private void Awake() {
        tileManager = GameObject.Find("Tile Manager").GetComponent<TileManager>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    /***********************************************
     * Methods to instantiate a new attack pattern *
     ***********************************************/

    public void Shockwave(int dmg) {
        damage = dmg;
        StartCoroutine(Attack_Shockwave());
    }

    public void Column(int dmg) {
        damage = dmg;
        StartCoroutine(Attack_Column());
    }

    public void Row(int numOfRowsToLob, int dmg) {
        damage = dmg;
        StartCoroutine(Attack_Row(numOfRowsToLob));
    }

    public void Sword(int dmg) {
        damage = dmg;
        StartCoroutine(Attack_Sword());
    }

    public void Grenade(int numOfRowsToLob ,int dmg) {
        damage = dmg;
        StartCoroutine(Attack_Grenade(numOfRowsToLob));
    }

    public void Boomerang(int dmg) {
        damage = dmg;
        StartCoroutine(Attack_Boomerang());
    }


    /*********************************************
     * Coroutines to perform each attack pattern *
     *********************************************/ 

    IEnumerator Attack_Shockwave() {
        for (int i = 0; i < tileManager.Width; i++) {
            ArrayList tilesToHit = GetTilesInRow(i); 
            StartCoroutine(AttackInGrouping(tilesToHit, .20f));
            yield return new WaitForSeconds(.20f);
        }
        yield return "hit";
    }
   
    IEnumerator Attack_Column() {
        ArrayList tilesToHit = GetTilesInColumn();
        StartCoroutine(AttackInGrouping(tilesToHit));
        yield return "hit";
    }

    IEnumerator Attack_Row(int numOfRowsToLob) {
        ArrayList tilesToHit = GetTilesInRow(numOfRowsToLob);
        StartCoroutine(AttackInGrouping(tilesToHit));
        yield return "hit";

    }

    IEnumerator Attack_Sword() {
        ArrayList tilesToHit = new ArrayList();
        tilesToHit.Add(tileManager.GetTileAtCoords(player.CurrentX + 1, player.CurrentZ));
        StartCoroutine(AttackInGrouping(tilesToHit));
        yield return "hit";
    }

    IEnumerator Attack_Grenade(int numSpaceFromPlayerToCenter) {
        ArrayList tilesToHit = new ArrayList();
        int currentPlayerX = player.CurrentX;
        int currentPlayerZ = player.CurrentZ;

        tilesToHit.Add(tileManager.GetTileAtCoords(currentPlayerX + numSpaceFromPlayerToCenter, currentPlayerZ - 1));
        tilesToHit.Add(tileManager.GetTileAtCoords(currentPlayerX + numSpaceFromPlayerToCenter, currentPlayerZ));
        tilesToHit.Add(tileManager.GetTileAtCoords(currentPlayerX + numSpaceFromPlayerToCenter, currentPlayerZ + 1));
        tilesToHit.Add(tileManager.GetTileAtCoords(currentPlayerX + numSpaceFromPlayerToCenter -1, currentPlayerZ));
        tilesToHit.Add(tileManager.GetTileAtCoords(currentPlayerX + numSpaceFromPlayerToCenter + 1, currentPlayerZ));

        StartCoroutine(AttackInGrouping(tilesToHit));
        yield return "hit";
    }

    IEnumerator Attack_Boomerang() {
        ArrayList tilesToHit = new ArrayList();
        int currentPlayerX = player.CurrentX;
        int currentPlayerZ = player.CurrentZ;
        float movementSpeed = .1f;

        //attack outwards
        for (int i = currentPlayerX; i < tileManager.Width; i++) {
            tilesToHit.Add(tileManager.GetTileAtCoords(i, currentPlayerZ));
            StartCoroutine(AttackInGrouping(tilesToHit, movementSpeed));
            yield return new WaitForSeconds(movementSpeed);
            tilesToHit.Clear();
        }

        //attack inwards
        for (int i = tileManager.Width; i > 0; i--) {
            tilesToHit.Add(tileManager.GetTileAtCoords(i, currentPlayerZ));
            StartCoroutine(AttackInGrouping(tilesToHit, movementSpeed));
            yield return new WaitForSeconds(movementSpeed);
            tilesToHit.Clear();
        }
    }

    /*********************
     * Utility Functions *
     *********************/


    //changes the colors of the tiles passed in for .5 seconds (or passed in duration)
    //to signify a player's attack then changes back to white
    IEnumerator AttackInGrouping(ArrayList tilesToHit, float waitDelay = .5f) {
        ChangeTileColors(tilesToHit, "attack");
        DamageOccupants(tilesToHit);
        yield return new WaitForSeconds(waitDelay);
        ChangeTileColors(tilesToHit, "");
        yield return "hit";
    }

    //changes colors of tiles in array based on whether the "attack" parameter is passed in or not
    //TODO: find a non-String way to do this
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

    private void DamageOccupants(ArrayList tilesToHit) {
        foreach (GameObject tile in tilesToHit) {
            if (tile != null) {
                GameObject tile_occupant = tile.GetComponent<Tile>().CurrentOccupant;
                if (tile_occupant != null) {
                    tile_occupant.GetComponent<Enemy>().DealDamage(damage);
                }
            }
        }
    }

    private ArrayList GetTilesInColumn() {
        ArrayList tilesToHit = new ArrayList();
        int currentPlayerX = player.CurrentX;
        int currentPlayerZ = player.CurrentZ;

        tilesToHit.Add(tileManager.GetTileAtCoords(currentPlayerX + 1, currentPlayerZ));
        tilesToHit.Add(tileManager.GetTileAtCoords(currentPlayerX + 2, currentPlayerZ));
        tilesToHit.Add(tileManager.GetTileAtCoords(currentPlayerX + 3, currentPlayerZ));

        return tilesToHit;
    }

    private ArrayList GetTilesInRow(int numSpacesFromPlayerToRow) {
        ArrayList tilesToHit = new ArrayList();
        int currentPlayerX = player.CurrentX;
        int currentPlayerZ = player.CurrentZ;

        tilesToHit.Add(tileManager.GetTileAtCoords(currentPlayerX + numSpacesFromPlayerToRow, currentPlayerZ-1));
        tilesToHit.Add(tileManager.GetTileAtCoords(currentPlayerX + numSpacesFromPlayerToRow, currentPlayerZ));
        tilesToHit.Add(tileManager.GetTileAtCoords(currentPlayerX + numSpacesFromPlayerToRow, currentPlayerZ+1));

        return tilesToHit;
    }
}
