using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCard : Card {

    public enum AttackPattern { Row, Column, Shockwave, Grenade, Sword, Boomerang, XSlash, PBAoE}

    public int damage = 50;
    Color tileColor;
    AttackCoroutines attack;

    public AttackPattern CardAttackType { get; set; }

    private void Start() {
        attack = gameObject.AddComponent<AttackCoroutines>();
    }

    public override void DoAction() {
        switch (CardAttackType) {
            case AttackPattern.Column: {
                    attack.Column();
                    break;
                }

            case AttackPattern.Row: {
                    attack.Row(3);
                    break;
                }

            case AttackPattern.Shockwave: {
                    attack.Shockwave();
                    break;
                }
        }
    }
}
