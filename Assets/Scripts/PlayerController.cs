using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public TileManager tileManager;
    public CardManager cardManager;
    public int numOfRowsToLob = 3;
    public float waitTime = .1f;

    AttackCoroutines attack;
    int currentX;
    int currentZ;
    bool allowMove = true;

    // Use this for initialization
    void Start () {
        tileManager = GameObject.Find("Tile Manager").GetComponent<TileManager>();
        cardManager = GameObject.Find("Card Manager").GetComponent<CardManager>();
	}
	
	// Update is called once per frame
	void Update () {
        HandleInputs();

        if(allowMove){
            StartCoroutine(MovePlayer());
        } 
        
	}

    private void HandleInputs() {

        //column attack pattern bound to Left Ctrl
        //in the future this will be dependent on the active card a player has and not bound to a keyboard key
        if (Input.GetButtonDown("Clockwise")) {
            cardManager.CycleCardsClockwise();
        }

        //
       if (Input.GetButtonDown("CounterClockwise")) {
            cardManager.CycleCardsCounterClockwise();
        }

        //perform attack with spacebar
        if (Input.GetButtonDown("A")) {
            cardManager.UseActiveCard();
        }
    }

    public static float GetAbsMax(float first, float second) {
        if (Math.Abs(first) > Math.Abs(second)) {
            return first;
        }

        else if (Math.Abs(first) < Math.Abs(second)) {
            return second;
        }
        else {
            return 0;
        }
    }

    //refactored to be a coroutine
    //with no allowMove variable, the player moves across the battlefield at lightning speed
    //so it was added to slow the player down to a more reasonable pace
    //which is dictated by waitTime seconds between each movement
    IEnumerator MovePlayer() {
        float startX = this.transform.position.x;
        float startZ = this.transform.position.z;
        float upDownInput = GetAbsMax(Input.GetAxisRaw("7"), Input.GetAxisRaw("Vertical"));
        float leftRightInput = GetAbsMax(Input.GetAxisRaw("6"), Input.GetAxisRaw("Horizontal"));


        //allowMove is only set to false if movement has occured
        //otherwise inputs would sometimes be eaten 
        if (leftRightInput < 0f && startX > 0) {
            startX--;
            allowMove = false;
        }

        if (leftRightInput > 0f && startX < 2) {
            startX++;
            allowMove = false;
        }

        if (upDownInput > 0f && startZ < 2) {
            startZ++;
            allowMove = false;
        }

        if (upDownInput < 0f && startZ > 0) {
            startZ--;
            allowMove = false;
        }

        this.transform.position = new Vector3(startX, 0, startZ);

        if (!allowMove) {
            yield return new WaitForSeconds(waitTime);
        }
        allowMove = true;

    }

    public int CurrentX {
        get {
            return (int)transform.position.x;
        }

        protected set { CurrentX = (int)transform.position.x; }
    }

    public int CurrentZ {
        get {
            return (int)transform.position.z;
        }

        protected set { CurrentZ = (int)transform.position.z; }

    }
}

