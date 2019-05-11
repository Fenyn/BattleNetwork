using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;
using System.IO;
using System;

public class BasicTurret : Enemy {

    int targetRow = 0;

    TileManager tm;

    // Use this for initialization
    void Start() {
        PandaBehaviour pb = gameObject.AddComponent<PandaBehaviour>();
        TextAsset ta = Resources.Load<TextAsset>("Scripts\\Utilities\\Combat\\Units\\Enemy Types\\BasicTurret");
        pb.Compile(ta.ToString());

        tm = GameObject.Find("Tile Manager").GetComponent<TileManager>();
    }

    // Update is called once per frame
    void Update() {
        if (CurrentHealth <= 0) {
            ui_manager.RemoveObjectWithHP(this);
            Destroy(gameObject);
        }
    }

    public BasicTurret(int maxHealthToSet = 70, int damagePerShotToSet = 15) {
        maxHealth = maxHealthToSet;
        DamagePerShot = damagePerShotToSet;
    }

    override public void Initialize() {
        maxHealth = 70;
        CurrentHealth = maxHealth;
        DamagePerShot = 15;
        ui_manager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        ui_manager.AddObjectWithHP(this);
    }

    IEnumerator AttackRoutine() {
        ArrayList tilesToHit = new ArrayList();
        int xCoord = CurrentX;
        int zCoord = CurrentZ;

        //attack from unit position to left edge of screen
        for (int i = CurrentX; i >= 0; i--) {
            tilesToHit.Add(tileManager.GetTileAtCoords(i, zCoord));
        }
        StartCoroutine(DamageTiles(tilesToHit));
        tilesToHit.Clear();

        yield return "hit";
    }

    IEnumerator ScanForAttack() {
        ArrayList tilesToHit = new ArrayList();
        int xCoord = CurrentX;
        int zCoord = CurrentZ;
        float movementSpeed = 0.2f;

        //attack from unit position to left edge of screen
        for (int i = CurrentX; i >= 0; i--) {
            tilesToHit.Add(tileManager.GetTileAtCoords(i, zCoord));
            GameObject go = tileManager.GetOccupantAtCoords(i, zCoord);
            if ( go != null && go.tag == "Player") {
                PlayerInRange = true;
            }
            ChangeTileColors(tilesToHit, "scan");
            yield return new WaitForSeconds(movementSpeed);
            ChangeTileColors(tilesToHit, "");
            tilesToHit.Clear();
        }

    }

    void TargetPlayerRow() {
        targetRow = FindPlayerRow();
    }

    #region tasks
    [Task]
    public bool PlayerInRange = false;

    //the attack for the basic shooter should shoot a bullet
    //towards the player starting from the enemies location
    [Task]
    public override void DoAttack() {
        StartCoroutine(AttackRoutine());
        PlayerInRange = false;
        Task.current.Succeed();
    }
    
    [Task]
    void CheckIfAbleToAttack() {
        StartCoroutine(ScanForAttack());
        if (PlayerInRange) {
            Task.current.Succeed();
        }
        else {
            Task.current.Fail();
        }
    }


    #endregion
}
