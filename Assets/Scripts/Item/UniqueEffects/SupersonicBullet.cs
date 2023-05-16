using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupersonicBullet : MonoBehaviour
{	
    private ItemType itemType;

    private void Start()
    {
        Bullet.C_BulletPierce += 2;
        Gun.C_BulletSpeed *= 1.5f;
        itemType = GetComponent<ItemType>();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (itemType.DestroyItem)
        {
            Bullet.C_BulletPierce -= 2;
            Gun.C_BulletSpeed /= 1.5f;
            Destroy(gameObject);
        }
    }
}