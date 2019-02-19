using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit {

	public enum Type { Blaster, Column, Row, Boomerang}

    private int damageAmount;

    public int DamageAmount { get; set; }

    private Type unitType;
    private AttackCard attackPattern;

    public Enemy(Type type, int damage) {
        unitType = type;
        DamageAmount = damage;
    }

    public void DoAttack() {
        attackPattern.DoAction();
    }

    private static GameObject enemyAttackObject;
    public static GameObject EnemyAttackObject {
        get {
            if (enemyAttackObject == null) {
                enemyAttackObject = new GameObject("Enemy Attack Object");
            }
            return enemyAttackObject;
        }
    }

    private void SetAttackBasedOnUnitType() {
        switch (unitType) {
            case Type.Blaster: {
                    attackPattern = MakeNewAttackCard(AttackCard.AttackPattern.Shockwave, DamageAmount);
                    break;
                }

            case Type.Boomerang:  {
                    attackPattern = MakeNewAttackCard(AttackCard.AttackPattern.Boomerang, DamageAmount);
                    break;
                }

            case Type.Column: {
                    attackPattern = MakeNewAttackCard(AttackCard.AttackPattern.Column, DamageAmount);
                    break;
                }

            case Type.Row: {
                    attackPattern = MakeNewAttackCard(AttackCard.AttackPattern.Row, DamageAmount);
                    break;
                }

            default: {
                    break;
                }
                        
        }
    }

    //factory pattern is used here because Unity hates doing a cards[x] = new AttackPattern(Row) sort of thing
    //so we instead add the card to a gameobject and return the object
    public static AttackCard MakeNewAttackCard(AttackCard.AttackPattern attackPattern, int damageValue) {
        var thisObj = EnemyAttackObject.AddComponent<AttackCard>();
        thisObj.CardAttackType = attackPattern;
        thisObj.damage = damageValue;

        return thisObj;
    }


}
