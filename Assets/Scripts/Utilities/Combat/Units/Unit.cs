using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
    [SerializeField]
    protected int maxHealth = 100;
    public int CurrentHealth { get; protected set; }
    public int CurrentX { get { return (int)transform.position.x; } }
    public int CurrentZ { get { return (int)transform.position.z; } }

    protected UIManager ui_manager;

    // Use this for initialization
    void Start() {
        CurrentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update() {
        if (CurrentHealth <= 0) {
            Debug.Log("Current health is: " + CurrentHealth);
            ui_manager.RemoveObjectWithHP(this);
            Destroy(gameObject);
        }
    }

    public void DealDamage(int damage) {
        CurrentHealth -= damage;
    }
}
