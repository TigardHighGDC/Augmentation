using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceShopItems : MonoBehaviour
{
    public Vector3[] Placements;
    public GameObject Pickupable;

    private HashSet<int> newItems = new HashSet<int>();

    private void Start()
    {
        GameObject[] Items = Resources.LoadAll<GameObject>("Item/Pickupable");
        foreach (Vector3 placement in Placements)
        {
            if (newItems.Count + ItemStorage.ItemPosition.Count >= Items.Length)
            {
                Debug.Log("break");
                break;
            }
            int random = Random.Range(0, newItems.Count + ItemStorage.ItemPosition.Count);
            while (newItems.Contains(random) || ItemStorage.ItemPosition.Contains(random))
            {
                random = Random.Range(0, newItems.Count + ItemStorage.ItemPosition.Count);
            }
            GameObject newPickup = Instantiate(Pickupable, placement, Quaternion.identity);
            newPickup.GetComponent<PickupableItem>().Item = Items[random];
            newItems.Add(Items[random].GetComponent<ItemType>().Index);
        }
    }

}
