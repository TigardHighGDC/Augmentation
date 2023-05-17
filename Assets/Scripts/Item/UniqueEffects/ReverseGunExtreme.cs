using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseGunExtreme : MonoBehaviour
{
    private ItemType itemType;

    private void Start()
    {
        Bullet.C_BulletBounce += 2;
        Gun.C_BulletSpeed *= 1.5f;
        itemType = GetComponent<ItemType>();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (itemType.DestroyItem)
        {
            Gun.C_BulletSpeed /= 1.5f;
            Bullet.C_BulletBounce -= 2;
            Destroy(gameObject);
        }
    }
}