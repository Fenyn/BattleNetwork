using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int maxHealth = 100;
    int CurrentHealth { get; set; }

	// Use this for initialization
	void Start () {
        CurrentHealth = maxHealth;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
