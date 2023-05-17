using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassCannon : MonoBehaviour
{
    private ItemType itemType;

    private void Start()
    {
        Bullet.C_Damage *= 1.50f;
        PlayerHealth.MaxHealth *= 0.50f;

        itemType = GetComponent<ItemType>();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (itemType.DestroyItem)
        {
            Bullet.C_Damage /= 1.50f;
            PlayerHealth.MaxHealth /= 0.50f;
            Destroy(gameObject);
        }
    }
}