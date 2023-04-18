using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTest : MonoBehaviour
{
    public List<GameObject> test;
    private ItemType itemType;
    // Start is called before the first frame update
    void Start()
    {
        itemType = GetComponent<ItemType>();
        Debug.Log("Hello");
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
