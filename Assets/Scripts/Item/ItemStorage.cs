using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStorage : MonoBehaviour
{
    public static ItemType Fragmentation;
    public static ItemType Weapon;
    public static List<GameObject> ItemList = new List<GameObject>();

    public void AddItemList(GameObject item)
    {
        ItemList.Add(item);
        item.GetComponent<ItemType>().Activate = true;
    }

    public void ReplaceItem(GameObject oldItem, GameObject newItem)
    {
        if (oldItem != null)
        {
            ItemType oldData  = oldItem.GetComponent<ItemType>();
            oldData.DestroyItem = true;
        }

        ItemType newData  = newItem.GetComponent<ItemType>();
        newData.Activate = true;
    }
}
