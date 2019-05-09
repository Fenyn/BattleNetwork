using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public int numberOfEnemiesToSpawn = 1;
    public GameObject enemy_prefab;
    TileManager tileManager;
    int minEnemyWidth = 3;
    int maxEnemyWidth = 5;
    int minEnemyHeight = 0;
    int maxEnemyHeight = 2;

    float timeCounter = 0f;


    GameObject[,] enemyGameObjects;
    Enemy[,] enemyDataObjects;

    // Use this for initialization
    void Start () {
        tileManager = GameObject.Find("Tile Manager").GetComponent<TileManager>();
        GenerateEnemies();
    }

    //get a random x,y pair, then try to spawn an enemy there
    //if an enemy already exists, spawn new x,y pair and try again
    private void GenerateEnemies() {
        int enemiesSpawned = 0;
        enemyGameObjects = new GameObject[6, 3];
        enemyDataObjects = new Enemy[6, 3];
        //TODO: optimize to better handle collisions, could theoretically spend a long time generating if board is full of junk
        while (enemiesSpawned < numberOfEnemiesToSpawn) {
            int x = GetRandomX();
            int z = GetRandomZ();
            if(enemyGameObjects[x,z] == null) {
                enemyGameObjects[x, z] = Instantiate(enemy_prefab, new Vector3(x, .3f, z), Quaternion.identity);
                enemyDataObjects[x, z] = enemyGameObjects[x,z].gameObject.AddComponent<BasicShooter>();
                enemyDataObjects[x, z].Initialize();
                tileManager.PutOccupantAtCoords(enemyGameObjects[x, z].gameObject, x, z);
                enemiesSpawned++;
            }

        }

        //log position of all new enemies
        foreach (GameObject enemy in enemyGameObjects) {
            if (enemy != null) {
                Debug.Log("Enemy spawned. Name: " + enemy.name + " Position: " + enemy.transform.position.x + ", " + enemy.transform.position.z);
            }
        }

        foreach (Enemy enemy in enemyDataObjects) {
            if(enemy != null) {

            Debug.Log("Enemy at " + enemy.CurrentX + ", " + enemy.CurrentZ);
            Debug.Log("Current health: " + enemy.CurrentHealth);
            Debug.Log("Damage per shot: " + enemy.DamagePerShot);
            }

        }
    }

    private int GetRandomX() {
        return UnityEngine.Random.Range(minEnemyWidth, maxEnemyWidth+1);
    }

    private int GetRandomZ() {
        return UnityEngine.Random.Range(minEnemyHeight, maxEnemyHeight+1);

    }

    public int GetRemainingEnemies()
    {
        int numEnemies = 0;
        foreach (GameObject enemy in enemyGameObjects) {
            if (enemy != null) {
                numEnemies++;
            }
        }

        return numEnemies;
    }

    // Update is called once per frame
    void Update () {
        //allows for user to respawn enemies when hitting '=' key
        if (Input.GetKeyDown(KeyCode.Equals) && GetRemainingEnemies() <= 0) {
            //Destroy all active enemies
            foreach (GameObject enemy in enemyGameObjects) {
                if(enemy != null) {
                    Destroy(enemy);
                }
            }
            
            //generate new enemy grid and populate board
            GenerateEnemies();
        }
        timeCounter += Time.deltaTime;
        if(timeCounter >= 3.0f) {
            foreach (Enemy enemy in enemyDataObjects) {
                if (enemy != null) {
                    //enemy.DoAttack();
                    //Debug.Log("SHOOTING!");
                }
            }
            timeCounter = 0.0f;
        }
	}
}
