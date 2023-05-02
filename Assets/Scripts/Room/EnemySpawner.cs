using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    public Tilemap TileMap;
    public EnemyType Enemy;
    public List<GameObject> BasicEnemies; 
    public List<GameObject> EliteEnemies; 

    private int basicEnemiesSpawn;
    private int eliteEnemiesSpawn;

    private void Awake()
    {
        switch (Enemy)
        {
            case EnemyType.Basic:
                basicEnemiesSpawn = 4 + Random.Range(-1, 2);
                eliteEnemiesSpawn = 1;
                break;
            case EnemyType.Elite:
                basicEnemiesSpawn = 6 + Random.Range(-1, 2);
                eliteEnemiesSpawn = 2 + Random.Range(0, 1);
                break;
        }
    }
    
    private void Start()
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

    private void SpawnEnemy(GameObject enemy)
    {
        BoundsInt boundary = TileMap.cellBounds;
        int xPosition = Random.Range(boundary.xMin + 1, boundary.xMax - 1);
        int yPosition = Random.Range(boundary.yMin + 1, boundary.yMax - 1);
        Vector3Int position = new Vector3Int(xPosition, yPosition, 0);
        Instantiate(enemy, (Vector3)position, Quaternion.identity);
    }
    
    public enum EnemyType
    {
        Basic,
        Elite
    }
}
