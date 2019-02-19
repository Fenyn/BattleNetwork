using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCard : Card {

    public enum AttackPattern { Row, Column, Shockwave, Grenade, Sword, Boomerang, XSlash, PBAoE}

    public AttackPattern CardAttackType { get; set; }

    public int damage { get; set; }

    AttackCoroutines attack;
    Color tileColor;

    private void Start() {
        attack = gameObject.AddComponent<AttackCoroutines>();
    }

    public override void DoAction() {
        switch (CardAttackType) {
            case AttackPattern.Column: {
                    attack.Column(damage);
                    break;
                }

            case AttackPattern.Row: {
                    attack.Row(3, damage);
                    break;
                }

            case AttackPattern.Shockwave: {
                    attack.Shockwave(damage);
                    break;
                }

            case AttackPattern.Sword: {
                    attack.Sword(damage);
                    break;
                }

            case AttackPattern.Grenade: {
                    attack.Grenade(3, damage);
                    break;
                }

            case AttackPattern.Boomerang: {
                    attack.Boomerang(damage);
                    break;
                }
        }
    }

    public override string GetCardTypeAsString() {
        switch (CardAttackType) {
            case AttackPattern.Column: {
                    return "Column";
                }

            case AttackPattern.Row: {
                    return "Row";
                }

            case AttackPattern.Shockwave: {
                    return "Shockwave";
                }

            case AttackPattern.Sword: {
                    return "Sword";
                }

            case AttackPattern.Grenade: {
                    return "Grenade";
                }

            case AttackPattern.Boomerang: {
                    return "Boomerang";
                }

            default: {
                    return "";
                }
        }
    }
}
