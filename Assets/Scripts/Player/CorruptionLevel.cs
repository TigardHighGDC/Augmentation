using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionLevel : MonoBehaviour
{
    private static float corruptionMax = 100f;
    private static float currentCorruption = 0f;

    public void Add(float increase)
    {
        currentCorruption = Mathf.Min(corruptionMax, currentCorruption + increase);
    }

    public float Level()
    {
        return currentCorruption;
    }
}
