using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weight : MonoBehaviour
{	
    private ItemType itemType;

    private void Start()
    {
        Gun.C_BulletSpeed *= 0.5f;
        Gun.C_Size *= 1.75f;
        itemType = GetComponent<ItemType>();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        
        if (itemType.DestroyItem)
        {
            Gun.C_BulletSpeed /= 0.5f;
            Gun.C_Size /= 1.75f;
            Destroy(gameObject);
        }
    }
}