using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTest2 : MonoBehaviour
{
    private ItemType itemType;
    // Start is called before the first frame update
    void Start()
    {
        itemType = GetComponent<ItemType>();
        Debug.Log("Hello2");
    }

    // Update is called once per frame
    void Update()
    {
        if (itemType.DestroyItem)
        {
            Destroy(gameObject);
        }
    }
}
