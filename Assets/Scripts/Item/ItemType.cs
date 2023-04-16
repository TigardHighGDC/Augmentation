using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemType : MonoBehaviour
{
    public string Name;

    [Header("Event")]
    public bool Activate;
    public bool DestroyItem;
    public ItemUse ItemPurpose;

    [Header("Description")]
    public string ItemStats;
    public string Lore;

    public enum ItemUse {
        Weapon, 
        Fragmentation,
        Pickupable
    }
}
