// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    public Tilemap TileMap;
    public RoomType Room;
    public List<GameObject> BasicEnemies;
    public List<GameObject> EliteEnemies;
    public static bool LoadingBarFinished = false;

    private float playerSpawnRadius = 5.0f;
    private float ambushProbability = 20;
    private GameObject player;

    private void Awake()
    {
        LoadingBarFinished = false;
        player = GameObject.FindGameObjectWithTag("Player");
        InitializeEnemies();
    }

    public bool Ambush()
    {
        // Removes corruption in exchange for possible enemy ambush
        CorruptionLevel.Add(-15.0f);
        int random = Random.Range(0, 101);
        if (random <= ambushProbability)
        {
            SpawnGroup(4, 2);
            return true;
        }

        ambushProbability *= 2;
        return false;
    }

    private void SpawnEnemy(GameObject enemy)
    {
        BoundsInt boundary = TileMap.cellBounds;
        int xPosition = Random.Range(boundary.xMin + 2, boundary.xMax - 2);
        int yPosition = Random.Range(boundary.yMin + 2, boundary.yMax - 2);
        Vector3Int position = new Vector3Int(xPosition, yPosition, 0);

        // Recalculate spawn position if too close to player.
        while (Vector3.Distance(player.transform.position, position) < playerSpawnRadius)
        {
            xPosition = Random.Range(boundary.xMin + 2, boundary.xMax - 2);
            yPosition = Random.Range(boundary.yMin + 2, boundary.yMax - 2);
            position = new Vector3Int(xPosition, yPosition, 0);
        }

        Instantiate(enemy, (Vector3)position, Quaternion.identity);
    }

    private void InitializeEnemies()
    {
        int basicEnemiesSpawn;
        int eliteEnemiesSpawn;
        switch (Room)
        {
            case RoomType.Basic:
                basicEnemiesSpawn = 5;
                eliteEnemiesSpawn = 1;
                SpawnGroup(basicEnemiesSpawn, eliteEnemiesSpawn);
                break;

            case RoomType.Elite:
                basicEnemiesSpawn = 10;
                eliteEnemiesSpawn = 2;
                SpawnGroup(basicEnemiesSpawn, eliteEnemiesSpawn);
                break;

            case RoomType.LoadingBar:
                InvokeRepeating("LoadingBarEnemySpawn", 0, 3.0f);
                break;
            default:
                break;
        }
    }

    private void SpawnGroup(int basicEnemiesSpawn, int eliteEnemiesSpawn)
    {
        for (int i = 0; i < basicEnemiesSpawn; i++)
        {
            int random = Random.Range(0, BasicEnemies.Count);
            SpawnEnemy(BasicEnemies[random]);
        }

        for (int i = 0; i < eliteEnemiesSpawn; i++)
        {
            int random = Random.Range(0, EliteEnemies.Count);
            SpawnEnemy(EliteEnemies[random]);
        }
    }

    private void LoadingBarEnemySpawn()
    {
        int weight = Random.Range(1, 7);

        if (weight <= 5)
        {
            int random = Random.Range(0, BasicEnemies.Count);
            SpawnEnemy(BasicEnemies[random]);
        }
        else
        {
            int random = Random.Range(0, EliteEnemies.Count);
            SpawnEnemy(EliteEnemies[random]);
        }

        if (LoadingBarFinished && Room == RoomType.LoadingBar)
        {
            EndLoadingBarEnemies();
        }
    }

    private void EndLoadingBarEnemies()
    {
        CancelInvoke("LoadingBarEnemySpawn");

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }

        LoadingBarFinished = false;
    }

    public enum RoomType
    {
        Basic,
        Elite,
        LoadingBar,
        CorruptionWell
    }
}
