using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStorage : MonoBehaviour
{
    public static GameObject Fragmentation = null;
    public static GameObject Weapon = null;
    public static List<GameObject> ItemList = new List<GameObject>();

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
}
