using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletPattern : MonoBehaviour
{
    public EnemyProjectileData SlowBulletData;
    public EnemyProjectileData FastBulletData;
    public EnemyProjectileData DieBulletData;

    [HideInInspector] public bool IsAttacking = false;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void SpawnBullet(Vector3 position, float rotation, EnemyProjectileData Data)
    {
        Quaternion eulerAngle = Quaternion.Euler(0, 0, rotation);

        // Spawn bullet and provide needed values
        GameObject bullet = Instantiate(Data.BulletPrefab, position, eulerAngle);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        bullet.GetComponent<EnemyBullet>().Data = Data;
        rb.velocity = bullet.transform.up * Data.BulletSpeed;
    }

    public IEnumerator IgnoringPhase()
    {
        IsAttacking = true;
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(0.15f);
            for (int bulletAmount = 0; bulletAmount < 6; bulletAmount++)
            {
                SpawnBullet(new Vector3(-10 + (bulletAmount * 4), 0, 0), 180, SlowBulletData);
            }
        }

        yield return new WaitForSeconds(0.35f);

        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(0.15f);
            for (int bulletAmount = 0; bulletAmount < 7; bulletAmount++)
            {
                SpawnBullet(new Vector3(-12 + (bulletAmount * 4), 0, 0), 180, SlowBulletData);
            }
        }

        yield return new WaitForSeconds(0.35f);
        IsAttacking = false;
    }

    public void DenialPhase()
    {
        Vector3 randomPoint = new Vector3(-7 + (Random.Range(0, 8)), 0, 0);
        Vector3 relativePoint =  randomPoint - player.transform.position;
        float rotation = Mathf.Atan2(relativePoint.y, relativePoint.x) * Mathf.Rad2Deg + 90;
        
        for (int bulletAngle = -1; bulletAngle < 2; bulletAngle++)
        {
            SpawnBullet(randomPoint, rotation + (bulletAngle * 22.5f), FastBulletData);
        }
    }
    
    public IEnumerator AngerPhase(float spawnPoint)
    {
        IsAttacking = true;
        StartCoroutine(AngerPhaseSecondaryAttack());
        for (int i = 0; i < 32; i++)
        {
            for (int u = 0; u < dieDictionary[i].Length; u++)
            {
                Vector3 newSpawn = new Vector3(spawnPoint + (dieDictionary[i][u] * 1.0f), 0, 0);
                SpawnBullet(newSpawn, 180, DieBulletData);
            }
            yield return new WaitForSeconds(0.075f);
        }
        yield return new WaitForSeconds(1.0f);
        IsAttacking = false;
    }

    public IEnumerator AngerPhaseSecondaryAttack()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int bulletAngle = -3; bulletAngle < 4; bulletAngle++)
            {
                SpawnBullet(new Vector3(0, -40, 0), bulletAngle * 15f, FastBulletData);
                yield return new WaitForSeconds(1.0f);
            }
            yield return new WaitForSeconds(2.3f);
        }
    }

    public IEnumerator BargainingPhase()
    {
        IsAttacking = true;
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.15f);
            SpawnBullet(new Vector3(-4, 0, 0), 180, SlowBulletData);
            SpawnBullet(new Vector3(4, 0, 0), 180, SlowBulletData);
        }

        for (int between = 0; between < 3; between++)
        {
            float randomDistance = Random.Range(1.0f, 7.0f);
            SpawnBullet(new Vector3(-12 + randomDistance + (between * 8), 0, 0), 180, SlowBulletData);
        }
        IsAttacking = false;

    }

    public IEnumerator DepressionPhase()
    {
        IsAttacking = true;
        int randomBulletLoss = Random.Range(0, 11);
        for (int i = 0; i < 17; i++)
        {
            Debug.Log(i);
            if (i < randomBulletLoss || i > randomBulletLoss + 6)
            {
                SpawnBullet(new Vector3(i * 1.5f - 12, 0, 0), 180, SlowBulletData);
            }
        }
         yield return new WaitForSeconds(2.0f);
        IsAttacking = false;
    }

    public void DepressionPhaseSecondaryAttack()
    {
        Vector3 randomPoint = new Vector3(-7 + (Random.Range(0, 8)), -30, 0);
        Vector3 relativePoint =  randomPoint - player.transform.position;
        float rotation = Mathf.Atan2(relativePoint.y, relativePoint.x) * Mathf.Rad2Deg + 90;
        
        for (int bulletAngle = -1; bulletAngle < 2; bulletAngle++)
        {
            SpawnBullet(randomPoint, rotation + (bulletAngle * 22.5f), FastBulletData);
        }
    }

    public IEnumerator AcceptancePhase()
    {
        IsAttacking = true;
        int xPosition = Random.Range(-12, 13);
        int yPosition = Random.Range(-32, -25);

        Vector3 bulletPosition = new Vector3(xPosition, yPosition, 0);
        Vector3 relativePoint =  bulletPosition - transform.position;
        float rotation = Mathf.Atan2(relativePoint.y, relativePoint.x) * Mathf.Rad2Deg + 90 + Random.Range(-10.0f, 10.0f);

        SpawnBullet(new Vector3(xPosition, yPosition, 0), rotation, SlowBulletData);
        yield return new WaitForSeconds(0.35f);
        IsAttacking = false;
    }

    private void DestroyAllBullets()
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
    }
        

    private Dictionary<int, int[]> dieDictionary = new Dictionary<int, int[]>()
    {
        {0, new int[]{0, 1, 2, 3, 4, 5}},
        {1, new int[]{0}},
        {2, new int[]{0}},
        {3, new int[]{0}},
        {4, new int[]{0, 1, 2, 3, 4, 5}},
        {5, new int[]{0}},
        {6, new int[]{0}},
        {7, new int[]{0}},
        {8, new int[]{0, 1, 2, 3, 4, 5}},
        {9, new int[]{}},
        {10, new int[]{}},
        {11, new int[]{0, 1, 2, 3, 4, 5,}},
        {12, new int[]{2, 3}},
        {13, new int[]{2, 3}},
        {14, new int[]{2, 3}},
        {15, new int[]{2, 3}},
        {16, new int[]{2, 3}},
        {17, new int[]{2, 3}},
        {18, new int[]{2, 3}},
        {19, new int[]{0, 1, 2, 3, 4, 5}},
        {20, new int[]{}},
        {21, new int[]{}},
        {22, new int[]{0, 1, 2, 3}},
        {23, new int[]{0, 4}},
        {24, new int[]{0, 5}},
        {25, new int[]{0, 5}},
        {26, new int[]{0, 5}},
        {27, new int[]{0, 5}},
        {28, new int[]{0, 5}},
        {29, new int[]{0, 5}},
        {30, new int[]{0, 4}},
        {31, new int[]{0, 1, 2, 3}}
        
    };
}
