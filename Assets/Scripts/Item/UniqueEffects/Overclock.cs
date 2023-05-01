// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overclock : MonoBehaviour
{
    private ItemType itemType;

    private void Start()
    {
        Gun.C_BulletPerTrigger *= 2;
        Gun.C_AmmoUsage *= 3;
        itemType = GetComponent<ItemType>();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (itemType.DestroyItem)
        {
            Gun.C_BulletPerTrigger /= 2;
            Gun.C_AmmoUsage /= 3;
            Destroy(gameObject);
        }
    }
}
