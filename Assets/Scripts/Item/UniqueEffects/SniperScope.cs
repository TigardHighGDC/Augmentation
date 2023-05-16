using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperScope : MonoBehaviour
{	
    private ItemType itemType;

    private void Start()
    {
        Bullet.C_DamgeOverRangeActive = true;

        itemType = GetComponent<ItemType>();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {   
        if (itemType.DestroyItem)
        {
            Bullet.C_DamgeOverRangeActive = false;
            Destroy(gameObject);
        }
    }
}