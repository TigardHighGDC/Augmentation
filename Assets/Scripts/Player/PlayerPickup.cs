using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public float pickupRange = 4.0f;
    private GameObject currentItem;
    private float itemDistance;

    void Update()
    {
        itemDistance = 99999f;
        currentItem = null;
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Pickupable"))
        {
            float distance = Vector3.Distance(transform.position, item.transform.position);

            if (distance < itemDistance)
            {  
                itemDistance = distance;
                currentItem = item;
            }
        }
        if (currentItem != null && itemDistance <= pickupRange && Input.GetKeyDown(KeyCode.F))
        {
            Pickup(currentItem.GetComponent<PickupableItem>());
            Destroy(currentItem);
        }
    }
    
    private void Pickup(PickupableItem pickup)
    {
        if (pickup.Weapon != null)
        {
            gameObject.GetComponent<WeaponInventory>().AddWeapon(pickup.Weapon, pickup.WeaponEffect);
        }
        else if (pickup.Item != null)
        {
            ItemStorage.ItemList.Add(ItemStorage.ReplaceItem(pickup.Item));
        }
    }
}
