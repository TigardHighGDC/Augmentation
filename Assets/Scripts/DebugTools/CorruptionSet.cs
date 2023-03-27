using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionSet : MonoBehaviour
{
    public float corruptionAdd;
    public float corruptionIncreaseInterval;

    private CorruptionLevel corruption;

    private void Start()
    {
        corruption = GetComponent<CorruptionLevel>();
        corruption.Add(corruptionAdd);
    }

    private void Update()
    {
        corruption.Add(corruptionIncreaseInterval * Time.deltaTime);
    }
}
