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

    private float ambushProbability = 0.1f;

    private void Awake()
    {
        LoadingBarFinished = false;
        InitializeEnemies();
    }

    public void Ambush()
    {
        // Removes corruption in exchange for possible enemy ambush
        CorruptionLevel.Add(-15.0f);
        
        if (Random.value <= ambushProbability)
        {
            SpawnGroup(8, 1);
        }
        ambushProbability *= 2;
    }

    private void SpawnEnemy(GameObject enemy)
    {
        BoundsInt boundary = TileMap.cellBounds;
        int xPosition = Random.Range(boundary.xMin + 1, boundary.xMax - 1);
        int yPosition = Random.Range(boundary.yMin + 1, boundary.yMax - 1);
        Vector3Int position = new Vector3Int(xPosition, yPosition, 0);
        Instantiate(enemy, (Vector3)position, Quaternion.identity);
    }

    private void InitializeEnemies()
    {
        int basicEnemiesSpawn;
        int eliteEnemiesSpawn;
        switch (Room)
        {
            case RoomType.Basic:
                basicEnemiesSpawn = 4 + Random.Range(-1, 2);
                eliteEnemiesSpawn = 1;
                SpawnGroup(basicEnemiesSpawn, eliteEnemiesSpawn); 
                break;

            case RoomType.Elite:
                basicEnemiesSpawn = 6 + Random.Range(-1, 2);
                eliteEnemiesSpawn = 2 + Random.Range(0, 1);
                SpawnGroup(basicEnemiesSpawn, eliteEnemiesSpawn); 
                break;
            
            case RoomType.LoadingBar:
                InvokeRepeating("LoadingBarEnemySpawn", 0, 2.5f);
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
