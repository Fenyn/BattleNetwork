using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShooter : Enemy {

	// Use this for initialization
	void Start () {
        maxHealth = 50;
        damagePerShot = 10;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public BasicShooter(int xCoord, int zCoord, int maxHealthToSet, int damagePerShotToSet) {
        this.CurrentX = xCoord;
        this.CurrentZ = zCoord;
        this.maxHealth = maxHealthToSet;
        this.damagePerShot = damagePerShotToSet;
    }

    //the attack for the basic shooter should shoot a bullet
    //towards the player starting from the enemies location
    public override void DoAttack() {
        ArrayList tilesToHit = new ArrayList();
        int xCoord = this.CurrentX;
        int zCoord = this.CurrentZ;
        float movementSpeed = .1f;

        //attack from unit position to left edge of screen
        for (int i = this.CurrentX; i > 0; i--) {
            tilesToHit.Add(tileManager.GetTileAtCoords(i, zCoord));
            StartCoroutine(this.AttackInGrouping(tilesToHit, movementSpeed));
            tilesToHit.Clear();
        }
    }

}
