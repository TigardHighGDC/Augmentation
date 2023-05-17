// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemType : MonoBehaviour
{
    public string Name;
    public Sprite Image;

    [Header("Event")]
    public bool DestroyItem = false;
    public ItemUse ItemPurpose;

    [Header("Description")]
    public string ItemStats;

    public float Cost = 0.0f;

    public enum ItemUse
    {
        Weapon,
        Fragmentation,
        Pickupable
    }
}
