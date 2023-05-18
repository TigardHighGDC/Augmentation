// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overload : MonoBehaviour
{
    private ItemType itemType;

    // Increases damage by 20%, but decreases accuracy by 35%
    private void Start()
    {
        itemType = GetComponent<ItemType>();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {

        if (itemType.DestroyItem)
        {
            Destroy(gameObject);
        }
    }
}
