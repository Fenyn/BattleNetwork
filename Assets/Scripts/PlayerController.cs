using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public TileManager tileManager;
    public int numOfRowsToLob = 3;
    int currentX;
    int currentZ;


    AttackCoroutines attack;

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

    // Use this for initialization
    void Start () {
        tileManager = GameObject.Find("Tile Manager").GetComponent<TileManager>();
        attack = gameObject.AddComponent<AttackCoroutines>();
        attack.TileManager = tileManager;
        attack.Player = this;
	}
	
	// Update is called once per frame
	void Update () {
        MovePlayer();
        HandleInputs();
	}

    private void HandleInputs() {

        //column attack pattern bound to Left Ctrl
        //in the future this will be dependent on the active card a player has and not bound to a keyboard key
        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            attack.Column();
        }

        //column attack pattern bound to Space
        //hits a row X spaces away from player where X = numOfRowsToLob
        //in the future this will be dependent on the active card a player has and not bound to a keyboard key
        if (Input.GetKeyDown(KeyCode.Space)) {
            attack.Row(numOfRowsToLob);
        }

        //shockwave attack pattern bound to Left Alt
        //sends a row attack forward from left side to right side
        //in the future this will be dependent on the active card a player has and not bound to a keyboard key
        if (Input.GetKeyDown(KeyCode.LeftAlt)) {
            attack.Shockwave();
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
}
