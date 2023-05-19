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
        int count = 0;
        GameObject[] Items = Resources.LoadAll<GameObject>("Item/Pickupable");
        foreach (Vector3 placement in Placements)
        {
            if (newItems.Count + ItemStorage.ItemPosition.Count >= Items.Length)
            {
                Debug.Log("break");
                break;
            }
            int random = Random.Range(0, Items.Length);
            while (newItems.Contains(random) || ItemStorage.ItemPosition.Contains(random))
            {
                Debug.Log(random);
                random = Random.Range(0, Items.Length);
                count += 1;
                if (count >= 100)
                {
                    Debug.Log("break");
                    break;
                }
            }
            GameObject newPickup = Instantiate(Pickupable, placement, Quaternion.identity);
            newPickup.GetComponent<PickupableItem>().Item = Items[random];
            newItems.Add(Items[random].GetComponent<ItemType>().Index);
        }
    }
}
