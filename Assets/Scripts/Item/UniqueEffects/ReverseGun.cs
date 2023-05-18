// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseGun : MonoBehaviour
{
    private ItemType itemType;

    private void Start()
    {
        Bullet.C_BulletBounce += 1;
        itemType = GetComponent<ItemType>();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (itemType.DestroyItem)
        {
            Bullet.C_BulletBounce -= 1;
            Destroy(gameObject);
        }
    }
}
