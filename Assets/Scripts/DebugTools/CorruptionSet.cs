using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionSet : MonoBehaviour
{
    public float corruptionAdd;

    private CorruptionLevel corruption;

    void Start()
    {
        corruption = GetComponent<CorruptionLevel>();
        corruption.Add(corruptionAdd);
    }
}
