using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAI : MonoBehaviour
{
    public EnemyProjectileData Data;
    public GameObject Bullet;
    protected GameObject player;
    protected AIPhysics aiPath;
    protected Rigidbody2D rb;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        aiPath = GetComponent<AIPhysics>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected float DistanceBetweenPlayer()
    {
        float y2 = player.transform.position.y;
        float y1 = transform.position.y;
        float x2 = player.transform.position.x;
        float x1 = transform.position.x;
        return Mathf.Sqrt(Mathf.Pow(y2 - y1, 2) + Mathf.Pow(x2 - x1, 2));
    }

    protected void Fire()
    {
        // Get angle to fire at player and convert to euler.
        Vector3 relativePoint = transform.position - player.transform.position;
        float rotation = Mathf.Atan2(relativePoint.y, relativePoint.x) * Mathf.Rad2Deg + 90;
        Quaternion eulerAngle = Quaternion.Euler(0, 0, rotation);

        // Spawn bullet and provide needed values.
        GameObject bullet = Instantiate(Bullet, transform.position, eulerAngle);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        bullet.GetComponent<LaptopGoonBullet>().Data = Data;
        rb.velocity = bullet.transform.up * Data.BulletSpeed;
    }
}
