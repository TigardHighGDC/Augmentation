// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStorage : MonoBehaviour
{
    public static GameObject Fragmentation = null;
    public static GameObject Weapon = null;
    public static List<GameObject> ItemList = new List<GameObject>();
    public static List<int> ResourceItemIndex = new List<int>();

    // Position for item placement on screen
    private static int uiX = 0;
    private static int uiY = 0;
    private static int uiMax = 8;
    private static int separatedDistance = 35;

    public static GameObject ReplaceItem(GameObject newItem, GameObject oldItem = null)
    {
        if (oldItem != null)
        {
            DeleteItem(oldItem);
        }

        GameObject created = Instantiate(newItem);

        return created;
    }

    public static GameObject DeleteItem(GameObject oldItem)
    {
        oldItem.GetComponent<ItemType>().DestroyItem = true;
        return null;
    }

    public static Vector2 ItemUIPosition()
    {
        Vector2 addedPosition = new Vector2(uiX * separatedDistance, uiY * separatedDistance * -1);
        uiX += 1;

        if (uiX >= uiMax)
        {
            uiX = 0;
            uiY += 1;
        }

        return addedPosition;
    }
}
