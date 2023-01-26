using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ItemHandling.TestEvent += Explode;   
    }

    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            ItemHandling.TestEvent?.Invoke();
        }
    }
    void Explode()
    {
        Debug.Log("BOOOOOOOM");
    }
}
