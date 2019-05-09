using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;
using System.IO;

public class BasicShooter : Enemy {

    int targetRow = 0;

	// Use this for initialization
	void Start () {
        
        PandaBehaviour pb = gameObject.AddComponent<PandaBehaviour>();
        pb.scripts[0] = Resources.Load<TextAsset>("Assets\\Scripts\\Utilities\\Combat\\Units\\Enemy Types\\BasicShooter.txt");
        //TODO: this isn't working
     
	}

    string readBehaviourTreeToString(string file_path) {
        StreamReader inp_stm = new StreamReader(file_path);
        string output = "";

        while (!inp_stm.EndOfStream) {
            string inp_ln = inp_stm.ReadLine();
            // Do Something with the input. 
            output += inp_ln;
        }

        inp_stm.Close();
        return output;
    }
	
	// Update is called once per frame
	void Update () {
        if (CurrentHealth <= 0) {
            Debug.Log("Current health is: " + CurrentHealth);
            ui_manager.RemoveObjectWithHP(this);
            Destroy(gameObject);
        }
    }

    public BasicShooter(int maxHealthToSet = 50, int damagePerShotToSet = 10) {
        maxHealth = maxHealthToSet;
        DamagePerShot = damagePerShotToSet;
    }

    override public void Initialize() {
        maxHealth = 50;
        CurrentHealth = maxHealth;
        DamagePerShot = 10;
        ui_manager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        ui_manager.AddObjectWithHP(this);
    }

    IEnumerator AttackRoutine() {
        ArrayList tilesToHit = new ArrayList();
        int xCoord = CurrentX;
        int zCoord = CurrentZ;
        float movementSpeed = 0.2f;

        //attack from unit position to left edge of screen
        for (int i = CurrentX; i >= 0; i--) {
            tilesToHit.Add(tileManager.GetTileAtCoords(i, zCoord));
            StartCoroutine(AttackInGrouping(tilesToHit, movementSpeed));
            yield return new WaitForSeconds(movementSpeed);
            tilesToHit.Clear();
        }
    }

    int FindPlayerRow() {
        GameObject player = GameObject.Find("Player");
        Debug.Log("Player current X coord: " + player.GetComponent<Player>().CurrentX);
        Debug.Log("Player current Z coord: " + player.GetComponent<Player>().CurrentZ);
        return 0;
    }

    #region tasks

    [Task]
    bool IsInPlayerRow = false;


    //the attack for the basic shooter should shoot a bullet
    //towards the player starting from the enemies location
    [Task]
    public override void DoAttack() {
        var task = Task.current;

        StartCoroutine(AttackRoutine());

        Task.current.Succeed();
    }

    [Task]
    void TargetPlayerRow() {
        targetRow = FindPlayerRow();
        Debug.Log("Target row: " + targetRow);
        Debug.Log("Current row: " + CurrentX);
        Task.current.Succeed();
    }

    [Task]
    void CheckIfNextMovePossible() {
        Debug.Log("Check if next move possible");
        Task.current.Succeed();
    }

    [Task]
    void MoveTowardsTargetRow() {
        Debug.Log("Move towards target row");
        Task.current.Succeed();
        if(CurrentX == targetRow) {
            IsInPlayerRow = true;
        }
    }

       
#endregion
}
