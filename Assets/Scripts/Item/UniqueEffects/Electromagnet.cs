using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electromagnet : MonoBehaviour
{
    private ItemType itemType;

    private void Start()
    {
        Bullet.C_Knockback *= -1;

        itemType = GetComponent<ItemType>();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (itemType.DestroyItem)
        {
            Bullet.C_Knockback *= -1;
            Destroy(gameObject);
        }
    }
}