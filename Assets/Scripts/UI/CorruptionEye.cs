// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorruptionEye : MonoBehaviour
{
    [HideInInspector]
    public Image Img;

    private Sprite[] eyeList = new Sprite[10];

    private void Start()
    {
        eyeList = Resources.LoadAll<Sprite>("CorruptionEye");
        Img = GetComponent<Image>();
    }

    private void Update()
    {
        Img.sprite = eyeList[ClosestIndex(CorruptionLevel.currentCorruption, CorruptionLevel.corruptionMax)];
    }

    private int ClosestIndex(float current, float maxSize)
    {
        float percentage = current / maxSize;
        int index = (int)Mathf.Floor(percentage * (eyeList.Length - 1));
        return index;
    }
}
