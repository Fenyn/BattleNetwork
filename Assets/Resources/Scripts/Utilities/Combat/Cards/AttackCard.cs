using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCard : Card {

    public enum AttackPattern { Row, Column, Shockwave, Grenade, Sword, Boomerang, XSlash, PBAoE}

    public AttackPattern CardAttackType { get; set; }

    public int Damage { get; set; }

    AttackCoroutines attacks;

    private void Start() {
        attacks = gameObject.AddComponent<AttackCoroutines>();
    }

    public override void DoAction() {
        switch (CardAttackType) {
            case AttackPattern.Column: {
                    attacks.Column(Damage);
                    break;
                }

            case AttackPattern.Row: {
                    attacks.Row(3, Damage);
                    break;
                }

            case AttackPattern.Shockwave: {
                    attacks.Shockwave(Damage);
                    break;
                }

            case AttackPattern.Sword: {
                    attacks.Sword(Damage);
                    break;
                }

            case AttackPattern.Grenade: {
                    attacks.Grenade(3, Damage);
                    break;
                }

            case AttackPattern.Boomerang: {
                    attacks.Boomerang(Damage);
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
