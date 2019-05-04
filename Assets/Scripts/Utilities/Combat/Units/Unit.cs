using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public int maxHealth = 100;
    public int CurrentHealth { get; protected set; }

    UIManager ui_manager;

	// Use this for initialization
	void Start () {
        ui_manager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        CurrentHealth = maxHealth;
        ui_manager.AddObjectWithHP(this);
    }
	
	// Update is called once per frame
	void Update () {
		if(CurrentHealth <= 0) {
            ui_manager.RemoveObjectWithHP(this);
            Destroy(gameObject);
        }
	}

    public void DealDamage(int damage) {
        CurrentHealth -= damage;
    }
}
