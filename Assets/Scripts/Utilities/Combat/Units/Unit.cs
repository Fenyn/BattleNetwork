using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
    [SerializeField]
    protected int maxHealth = 100;
    public int CurrentHealth { get; protected set; }

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
