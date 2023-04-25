using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEffectTest : MonoBehaviour
{
    private ItemType itemType;

    void Start()
    {
        // Apply stat and event changes here.

        itemType = GetComponent<ItemType>();
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        // Add gameplay changes here.

        if (itemType.DestroyItem)
        {
            // Remove stat and event changes here.
            Destroy(gameObject);
        }
    }
}