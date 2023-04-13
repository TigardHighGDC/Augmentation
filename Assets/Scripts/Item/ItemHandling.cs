using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandling : MonoBehaviour
{
    public delegate void BasicEventHandler();
    
    public static BasicEventHandler BulletHit;
}
