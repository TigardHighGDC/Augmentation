using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteForceAttack : MonoBehaviour
{
    private ItemType itemType;
    private Gun gun;
    private void Start()
    {
        gun = GameObject.FindGameObjectWithTag("Player").GetComponent<Gun>();
        Gun.C_Spread *= 2;
        itemType = GetComponent<ItemType>();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        gun.AmmoAmount = gun.Data.AmmoCapacity * Gun.C_AmmoCapacity;

        if (itemType.DestroyItem)
        {
            Gun.C_Spread /= 2;
            Destroy(gameObject);
        }
    }
}