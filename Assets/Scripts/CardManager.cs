using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {

    Card[] cards;
    Card activeCard;
    int currentCardIndex = 0;
    AttackCoroutines attack;
    UIManager uiManager;


    private static GameObject managerObj;
    public static GameObject ManagerObj {
        get {
            if (managerObj == null) {
                managerObj = new GameObject("Manager Object");
            }
            return managerObj;
        }
    }

    private void Awake() {
        cards = new Card[6];
        cards[0] = MakeNewAttackCard(AttackCard.AttackPattern.Column, 50);
        cards[1] = MakeNewAttackCard(AttackCard.AttackPattern.Row, 50);
        cards[2] = MakeNewAttackCard(AttackCard.AttackPattern.Shockwave, 35);
        cards[3] = MakeNewAttackCard(AttackCard.AttackPattern.Sword, 100);
        cards[4] = MakeNewAttackCard(AttackCard.AttackPattern.Grenade, 100);
        cards[5] = MakeNewAttackCard(AttackCard.AttackPattern.Boomerang, 50);
        activeCard = cards[currentCardIndex];
    }

    // Use this for initialization
    void Start () {
        uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
    }

    //factory pattern is used here because Unity hates doing a cards[x] = new AttackPattern(Row) sort of thing
    //so we instead add the card to a gameobject and return the object
    public static Card MakeNewAttackCard(AttackCard.AttackPattern attackPattern, int damageValue) {
        var thisObj = ManagerObj.AddComponent<AttackCard>();
        thisObj.CardAttackType = attackPattern;
        thisObj.damage = damageValue;

        return thisObj;
    }
    
    public void UseActiveCard() {
        activeCard.DoAction();
        Debug.Log("Card used. new index: " + currentCardIndex);
    }

    public void CycleCardsClockwise() {
        currentCardIndex++;
        if(currentCardIndex >= cards.Length) {
            currentCardIndex = 0;
        }
        activeCard = cards[currentCardIndex];
        uiManager.UpdateCurrentCard();
        Debug.Log("Cards cycled clockwise. new index: " + currentCardIndex);
    }

    public void CycleCardsCounterClockwise() {
        currentCardIndex--;
        if (currentCardIndex < 0) {
            currentCardIndex += cards.Length;
        }
        activeCard = cards[currentCardIndex];
        uiManager.UpdateCurrentCard();

        Debug.Log("Cards cycled counter clockwise. new index: " + currentCardIndex);
    }

    public string GetCurrentCardNameAsString() {
        return cards[currentCardIndex].GetCardTypeAsString();
    }
}
