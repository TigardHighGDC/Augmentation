using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    public Camera Camera;

    // Update is called once per frame
    void Update()
    {
        var mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
        var relativePoint = transform.position - mousePosition;
        transform.rotation =  Quaternion.Euler(0, 0, Mathf.Atan2(relativePoint.y, relativePoint.x) * Mathf.Rad2Deg + 90);
    }
}
