using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Start()
    {
        Invoke("DestroyBullet", 10f);
    }

    void OnTriggerEnter2D(Collider2D collide)
    {
        if (collide.gameObject.tag != "Bullet")
        {
            Destroy(collide.gameObject);
        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
