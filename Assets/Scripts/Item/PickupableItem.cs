using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupableItem : MonoBehaviour
{

    public WeaponData Weapon = null;
    public GameObject WeaponEffect = null;

    public GameObject Item = null;

    void Start()
    {
        SpriteRenderer spriteRender = GetComponent<SpriteRenderer>();
        if (Weapon != null)
        {
            spriteRender.sprite = Weapon.Image;
        }
        else if (Item != null)
        {
            spriteRender.sprite = Item.GetComponent<ItemType>().Image;
        }
        else
        {
            Debug.LogError("Pickupable hasn't been assigned value");
        }
    }
}
