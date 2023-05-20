using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingBarDoor : MonoBehaviour
{
    public LoadingBarRoom LoadingBarRoom;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (LoadingBarRoom.EndLoading)
        {
            GetComponent<Door>().Unlock();
        }
        else
        {
            GetComponent<Door>().Lock();
        }
    }
}
