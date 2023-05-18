// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerPickup : MonoBehaviour
{
    public GameObject UIParent;
    public GameObject ItemUI;

    private float pickupRange = 6.5f;
    private GameObject currentItem;
    private float itemDistance;
    private GameObject itemDescription;

    private void Start()
    {
        GameObject prefabUI = Resources.Load<GameObject>("Item/ItemDescriptionCanvas");
        itemDescription = Instantiate(prefabUI, Vector3.zero, Quaternion.identity);
    }

    private void Update()
    {
        itemDistance = 99999.0f;
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

        if (itemDistance <= pickupRange)
        {
            itemDescription.SetActive(true);
            DisplayDescription();
        }
        else
        {
            itemDescription.SetActive(false);
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
            GameObject itemUI = Instantiate(ItemUI, UIParent.transform);
            itemUI.GetComponent<RectTransform>().anchoredPosition += ItemStorage.ItemUIPosition();
            itemUI.GetComponent<Image>().sprite = pickup.Item.GetComponent<ItemType>().Image;
            ItemStorage.ItemList.Add(ItemStorage.ReplaceItem(pickup.Item));
        }
    }

    private void DisplayDescription()
    {
        PickupableItem itemPickup = currentItem.GetComponent<PickupableItem>();
        if (itemPickup.Item != null)
        {
            ItemType itemType = itemPickup.Item.GetComponent<ItemType>();
            RectTransform descriptionPosition = itemDescription.GetComponent<RectTransform>();
            descriptionPosition.anchoredPosition = currentItem.transform.position + new Vector3(0.0f, -2.0f, 0.0f);

            foreach (TextMeshProUGUI textUI in itemDescription.GetComponentsInChildren<TextMeshProUGUI>())
            {
                textUI.text = $"{itemType.Name}: {itemType.ItemStats}";
            }
        }
    }
}
