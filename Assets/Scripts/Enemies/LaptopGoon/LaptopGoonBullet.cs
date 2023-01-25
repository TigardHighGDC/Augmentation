using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopGoonBullet : MonoBehaviour
{
    [HideInInspector]
    public float Damage;

    private void Start()
    {
        Invoke("DestroyBullet", 10f);
    }

    private void OnTriggerEnter2D(Collider2D collide)
    {
        if (collide.gameObject.tag == "Player")
        {
            PlayerHealth playerHealth = collide.gameObject.GetComponent<PlayerHealth>();
            playerHealth.Damage(Damage);
            DestroyBullet();
        }
    }

    // DestroyBullet() is called in the invoke function.
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
