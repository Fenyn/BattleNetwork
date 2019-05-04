using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float movementDelay = .1f;

    private Player player;
    private TileManager tileManager;
    private CardManager cardManager;

    bool allowMove = true;

    // Use this for initialization
    void Start () {
        tileManager = GameObject.Find("Tile Manager").GetComponent<TileManager>();
        cardManager = GameObject.Find("Card Manager").GetComponent<CardManager>();
        player = GameObject.Find("Player").GetComponent<Player>();

    }

    // Update is called once per frame
    void Update () {
        HandleInputs();

        if(allowMove){
            StartCoroutine(MovePlayer());
        } 
        
	}

    private void HandleInputs() {

        if (Input.GetButtonDown("Clockwise")) {
            cardManager.CycleCardsClockwise();
        }

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
            return first;
        }
    }

    //refactored to be a coroutine
    //issue: with no allowMove variable, the player moves across the battlefield at lightning speed
    //so it was added to slow the player down to a more reasonable pace
    //which is dictated by waitTime seconds between each movement
    IEnumerator MovePlayer() {
        float xCoord = player.CurrentX;
        float zCoord = player.CurrentZ;
        float upDownInput = GetAbsMax(Input.GetAxisRaw("7"), Input.GetAxisRaw("Vertical"));
        float leftRightInput = GetAbsMax(Input.GetAxisRaw("6"), Input.GetAxisRaw("Horizontal"));


        //allowMove is only set to false if movement has occured
        //otherwise inputs would sometimes be eaten 
        if (leftRightInput < 0f && xCoord > 0) {
            xCoord--;
            allowMove = false;
        }

        if (leftRightInput > 0f && xCoord < 2) {
            xCoord++;
            allowMove = false;
        }

        if (upDownInput > 0f && zCoord < 2) {
            zCoord++;
            allowMove = false;
        }

        if (upDownInput < 0f && zCoord > 0) {
            zCoord--;
            allowMove = false;
        }

        this.transform.position = new Vector3(xCoord, 0, zCoord);

        if (!allowMove) {
            yield return new WaitForSeconds(movementDelay);
        }
        allowMove = true;

    }
}

