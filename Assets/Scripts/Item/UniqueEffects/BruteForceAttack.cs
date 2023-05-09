// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteForceAttack : MonoBehaviour
{
    private ItemType itemType;
    private Gun gun;

    private void Start()
    {
        Gun.C_Spread *= 2;
        itemType = GetComponent<ItemType>();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        gun = GameObject.FindGameObjectWithTag("Player").GetComponent<Gun>();
        gun.AmmoAmount = gun.Data.AmmoCapacity * Gun.C_AmmoCapacity;

        if (itemType.DestroyItem)
        {
            Gun.C_Spread /= 2;
            Destroy(gameObject);
        }
    }
}
