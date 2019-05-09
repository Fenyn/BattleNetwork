using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

    [SerializeField]
    public Grid grid;

    public WorldTile[,] gameMap;

    public GameObject CubePrefab;

    public int width = 3;
    public int height = 3;

	// Use this for initialization
	void Start () {
        grid = FindObjectOfType<Grid>();
        Debug.Log("World Manager - Grid found: " + grid.ToString());

        GenerateWorld(3,3, WorldType.Flat);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void GenerateWorld(int width, int height, WorldType type)
    {
        switch (type) {

            case WorldType.Flat:

                gameMap = new WorldTile[width, height];

                for (int x = 0; x < width; x++) {
                    for(int y = 0; y < height; y++) {
                        Vector3 pos = new Vector3(x, 0, y);
                        Vector3 spawnPoint = grid.GetNearestPointOnGrid(pos);
                        gameMap[x, y] = new WorldTile();
                        gameMap[x, y].SetTileGameObject(CubePrefab, spawnPoint);
                        Debug.Log("Spawning a tile at " + spawnPoint.ToString());
                    }
                }

                break;

            case WorldType.Rooms:


                break;
            default:
                break;
        }
    }
}

public enum WorldType { Flat, Snake, Rooms}
