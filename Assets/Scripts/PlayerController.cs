using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public TileManager tileManager;
    public CardManager cardManager;
    public int numOfRowsToLob = 3;

    AttackCoroutines attack;
    int currentX;
    int currentZ;

    

    // Use this for initialization
    void Start () {
        tileManager = GameObject.Find("Tile Manager").GetComponent<TileManager>();
        cardManager = GameObject.Find("Card Manager").GetComponent<CardManager>();
	}
	
	// Update is called once per frame
	void Update () {
        MovePlayer();
        HandleInputs();
	}

    private void HandleInputs() {

        //column attack pattern bound to Left Ctrl
        //in the future this will be dependent on the active card a player has and not bound to a keyboard key
        if (Input.GetKeyDown(KeyCode.E)) {
            cardManager.CycleCardsClockwise();
        }

        //
       if (Input.GetKeyDown(KeyCode.Q)) {
            cardManager.CycleCardsCounterClockwise();
        }

        //perform attack with spacebar
        if (Input.GetKeyDown(KeyCode.Space)) {
            cardManager.UseActiveCard();
        }
    }

    void MovePlayer() {
        float startX = this.transform.position.x;
        float startZ = this.transform.position.z;

        if(Input.GetKeyDown(KeyCode.A) && startX > 0) {
            startX--;
        }

        if(Input.GetKeyDown(KeyCode.D) && startX < 2){
            startX++;
        }

        if(Input.GetKeyDown(KeyCode.W) && startZ < 2) {
            startZ++;
        }
        
        if(Input.GetKeyDown(KeyCode.S) && startZ > 0) {
            startZ--;
        }

        this.transform.position = new Vector3(startX, 0, startZ);
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

