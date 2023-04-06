// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using UnityEngine;

[System.Serializable]
public class IntMinMax
{
    // Intentionally ignoring naming conventions to align with Unity packages
    public int min;
    public int max;

    public IntMinMax()
    {
        min = 0;
        max = 0;
    }

    public IntMinMax(int min, int max)
    {
        this.min = min;
        this.max = max;
    }

    public int GetValue()
    {
        return Random.Range(min, max + 1);
    }
}

[System.Serializable]
public class FloatMinMax
{
    // Intentionally ignoring naming conventions to align with Unity packages
    public float min;
    public float max;

    public FloatMinMax()
    {
        min = 0;
        max = 0;
    }

    public FloatMinMax(float min, float max)
    {
        this.min = min;
        this.max = max;
    }

    public float GetValue()
    {
        return Random.Range(min, max);
    }
}
