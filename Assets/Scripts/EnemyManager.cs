using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public int numberOfEnemiesToSpawn = 1;
    public GameObject enemy_prefab;
    int minEnemyWidth = 3;
    int maxEnemyWidth = 5;
    int minEnemyHeight = 0;
    int maxEnemyHeight = 2;

    GameObject[,] enemies;

    // Use this for initialization
    void Start () {
        enemies = new GameObject[6, 3];
        GenerateEnemies();
        foreach (GameObject enemy in enemies) {
            if(enemy != null) {
                Debug.Log("Enemy spawned. Name: " + enemy.name + " Position: " + enemy.transform.position.x + ", " + enemy.transform.position.z);
            }
        }
	}

    private void GenerateEnemies() {
        int enemiesSpawned = 0;
        while (enemiesSpawned < numberOfEnemiesToSpawn) {
            int x = GetRandomX();
            int y = GetRandomY();
            if(enemies[x,y] == null) {
                enemies[x, y] = Instantiate(enemy_prefab, new Vector3(x, .3f, y), Quaternion.identity);
                enemiesSpawned++;
            }
        }
    }

    private int GetRandomX() {
        return UnityEngine.Random.Range(minEnemyWidth, maxEnemyWidth+1);
    }

    private int GetRandomY() {
        return UnityEngine.Random.Range(minEnemyHeight, maxEnemyHeight+1);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Equals)) {
            foreach (GameObject enemy in enemies) {
                if(enemy != null) {
                    Destroy(enemy);
                }
            }
            enemies = new GameObject[6, 3];
            GenerateEnemies();
        }
	}
}
