using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemType : MonoBehaviour
{
    public string Name;
    public Sprite Image;
    public Sprite Text;

    [Header("Event")]
    public bool DestroyItem = false;
    public ItemUse ItemPurpose;

    [Header("Description")]
    public string ItemStats;
    public string Lore;

    public enum ItemUse
    {
        Weapon,
        Fragmentation,
        Pickupable
    }
}
