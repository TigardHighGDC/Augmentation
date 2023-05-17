using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubberBullet : MonoBehaviour
{
    private ItemType itemType;

    private void Start()
    {
        Bullet.C_Damage *= 0.5f;
        Bullet.C_Knockback *= 2.0f;
        itemType = GetComponent<ItemType>();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (itemType.DestroyItem)
        {
            Bullet.C_Damage /= 0.5f;
            Bullet.C_Knockback /= 2.0f;
            Destroy(gameObject);
        }
    }
}