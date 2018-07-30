using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {

    Card[] cards;
    Card activeCard;
    int currentCardIndex = 0;
    AttackCoroutines attack;


    private static GameObject managerObj;
    public static GameObject ManagerObj {
        get {
            if (managerObj == null) {
                managerObj = new GameObject("Manager Object");
            }
            return managerObj;
        }
    }



    // Use this for initialization
    void Start () {
        attack = gameObject.AddComponent<AttackCoroutines>();
        cards = new Card[3];
        cards[0] = MakeNewAttackCard(AttackCard.AttackPattern.Column);
        cards[1] = MakeNewAttackCard(AttackCard.AttackPattern.Row);
        cards[2] = MakeNewAttackCard(AttackCard.AttackPattern.Shockwave);
        activeCard = cards[currentCardIndex];
    }

    //factory pattern is used here because Unity hates doing an = new AttackPattern(Row) sort of thing
    //so we instead add the card to a gameobject and return the object
    public static Card MakeNewAttackCard(AttackCard.AttackPattern attackPattern) {
        var thisObj = ManagerObj.AddComponent<AttackCard>();
        thisObj.CardAttackType = attackPattern;

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
        Debug.Log("Cards cycled clockwise. new index: " + currentCardIndex);
    }

    public void CycleCardsCounterClockwise() {
        currentCardIndex--;
        if (currentCardIndex < 0) {
            currentCardIndex += cards.Length;
        }
        activeCard = cards[currentCardIndex];
        Debug.Log("Cards cycled counter clockwise. new index: " + currentCardIndex);
    }
}
