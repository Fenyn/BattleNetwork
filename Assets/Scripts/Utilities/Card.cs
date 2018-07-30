using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card {

    public enum AttackPattern { Row, Column, Shockwave, Grenade, Sword, Boomerang, XSlash, PBAoE}

    public int damage = 50;
    Color tileColor;

    AttackPattern cardAttackType;

    public Card(AttackPattern pattern) {
        cardAttackType = pattern;
    }

}
