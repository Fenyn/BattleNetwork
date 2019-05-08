using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit {

    protected TileManager tileManager;

    private void Awake() {
        tileManager = GameObject.Find("Tile Manager").GetComponent<TileManager>();
    }

    void Start() { }

    public int DamagePerShot { get; set; }

    virtual public void DoAttack() { }
    virtual protected void Move() { }
    virtual public void Initialize() { }

    public Enemy() { }

    /*********************
     * Utility Functions *
     *********************/


    //changes the colors of the tiles passed in for .5 seconds (or passed in duration)
    //to signify a player's attack then changes back to white
    protected IEnumerator AttackInGrouping(ArrayList tilesToHit, float waitDelay = 0.5f) {
        ChangeTileColors(tilesToHit, "attack");
        DamageOccupants(tilesToHit);
        yield return new WaitForSeconds(waitDelay);
        ChangeTileColors(tilesToHit, "");
        yield return "hit";
    }

    //changes colors of tiles in array based on whether the "attack" parameter is passed in or not
    //TODO: find a non-String way to do this
    private void ChangeTileColors(ArrayList tilesToHit, string type) {
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
                if (tile_occupant != null && tile_occupant.GetComponent<Player>() != null) {
                    tile_occupant.GetComponent<Unit>().DealDamage(DamagePerShot);
                }
            }
        }
    }

    private ArrayList GetTilesInColumn() {
        ArrayList tilesToHit = new ArrayList();
        int currentX = this.CurrentX;
        int currentZ = this.CurrentZ;

        tilesToHit.Add(tileManager.GetTileAtCoords(currentX + 1, currentZ));
        tilesToHit.Add(tileManager.GetTileAtCoords(currentX + 2, currentZ));
        tilesToHit.Add(tileManager.GetTileAtCoords(currentX + 3, currentZ));

        return tilesToHit;
    }

    private ArrayList GetTilesInRow(int numSpacesFromUnitToRow) {
        ArrayList tilesToHit = new ArrayList();
        int currentX = this.CurrentX;
        int currentZ = this.CurrentZ;

        tilesToHit.Add(tileManager.GetTileAtCoords(currentX - numSpacesFromUnitToRow, currentZ - 1));
        tilesToHit.Add(tileManager.GetTileAtCoords(currentX - numSpacesFromUnitToRow, currentZ));
        tilesToHit.Add(tileManager.GetTileAtCoords(currentX - numSpacesFromUnitToRow, currentZ + 1));

        return tilesToHit;
    }

}
