using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit {

    private int damageAmount;
    public int DamageAmount { get; set; }

    public Enemy(int damage) {
        DamageAmount = damage;
    }

    virtual public void DoAttack() { }
    virtual protected void Move() { }

}
