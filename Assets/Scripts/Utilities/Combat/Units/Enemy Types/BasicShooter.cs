using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShooter : Enemy {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (CurrentHealth <= 0) {
            Debug.Log("Current health is: " + CurrentHealth);
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

    //the attack for the basic shooter should shoot a bullet
    //towards the player starting from the enemies location
    public override void DoAttack() {
        StartCoroutine(AttackRoutine());
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

}
