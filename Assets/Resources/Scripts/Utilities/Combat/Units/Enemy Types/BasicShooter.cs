using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;
using System.IO;
using System;

public class BasicShooter : Enemy {

    int targetRow = 0;

    TileManager tm;

	// Use this for initialization
	void Start () {
        PandaBehaviour pb = gameObject.AddComponent<PandaBehaviour>();
        TextAsset ta = Resources.Load<TextAsset>("Scripts\\Utilities\\Combat\\Units\\Enemy Types\\BasicShooter");
        pb.Compile(ta.ToString());

        tm = GameObject.Find("Tile Manager").GetComponent<TileManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (CurrentHealth <= 0) {
            ui_manager.RemoveObjectWithHP(this);
            Destroy(gameObject);
        }
    }

    public BasicShooter(int maxHealthToSet = 50, int damagePerShotToSet = 10) {
        maxHealth = maxHealthToSet;
        DamagePerShot = damagePerShotToSet;
    }

    override public void Initialize() {
        maxHealth = 50;
        CurrentHealth = maxHealth;
        DamagePerShot = 10;
        ui_manager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        ui_manager.AddObjectWithHP(this);
    }

    IEnumerator AttackRoutine() {
        ArrayList tilesToHit = new ArrayList();
        int xCoord = CurrentX;
        int zCoord = CurrentZ;
        float movementSpeed = 0.2f;

        //attack from unit position to left edge of screen
        for (int i = CurrentX; i >= 0; i--) {
            tilesToHit.Add(tileManager.GetTileAtCoords(i, zCoord));
            StartCoroutine(AttackInGrouping(tilesToHit, movementSpeed));
            yield return new WaitForSeconds(movementSpeed);
            tilesToHit.Clear();
        }
    }

    int FindPlayerRow() {
        GameObject player = GameObject.Find("Player");
        return player.GetComponent<Player>().CurrentZ;
    }

    void TargetPlayerRow() {
        targetRow = FindPlayerRow();
    }

    private void MoveUpwards() {
        tm.RemoveOccupantAtCoords(CurrentX, CurrentZ);
        gameObject.transform.position = new Vector3(CurrentX, gameObject.transform.position.y, CurrentZ + 1);
        tm.PutOccupantAtCoords(gameObject, CurrentX, CurrentZ);
    }

    private void MoveDownwards() {
        tm.RemoveOccupantAtCoords(CurrentX, CurrentZ);
        gameObject.transform.position = new Vector3(CurrentX, gameObject.transform.position.y, CurrentZ - 1);
        tm.PutOccupantAtCoords(gameObject, CurrentX, CurrentZ);
    }

    #region tasks

    [Task]
    public bool IsInPlayerRow = false;

    //the attack for the basic shooter should shoot a bullet
    //towards the player starting from the enemies location
    [Task]
    public override void DoAttack() {
        StartCoroutine(AttackRoutine());
        Task.current.Succeed();
    }

    [Task]
    void MoveTowardsTargetRow() {
        Debug.Log("Move towards target row");
        if(targetRow > CurrentZ) {
            if (tm.GetOccupantAtCoords(CurrentX, CurrentZ + 1) != null) {
                Debug.Log("Occupant at: " + CurrentX + ", " + (CurrentZ + 1));
                Debug.Log("Failing task");
                Task.current.Fail();
            }
            else {
                MoveUpwards();
            }
        } else if (targetRow < CurrentZ) {
            if (tm.GetOccupantAtCoords(CurrentX, CurrentZ - 1) != null) {
                Debug.Log("Occupant at: " + CurrentX + ", " + (CurrentZ - 1));
                Debug.Log("Failing task");
                Task.current.Fail();
            }
            else {
                MoveDownwards();
            }
        }

        Task.current.Succeed();        
    }

    [Task]
    void CheckIfAbleToAttack() {
        TargetPlayerRow();
        if (CurrentZ == targetRow) {
            IsInPlayerRow = true;
            Task.current.Succeed();
        }
        else {
            IsInPlayerRow = false;
            Task.current.Fail();

        }

        Task.current.Succeed();
    }


    #endregion
}
