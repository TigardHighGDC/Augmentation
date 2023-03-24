using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorruptionEye : MonoBehaviour
{
    private Sprite[] eyeList = new Sprite[10];

    [HideInInspector]
    public Image Img;

    void Start()
    {
        eyeList = Resources.LoadAll<Sprite>("CorruptionEye");
        Img = GetComponent<Image>();
    }

    void Update()
    {
        Img.sprite = eyeList[ClosestIndex(CorruptionLevel.currentCorruption,  CorruptionLevel.corruptionMax)];
    }

    private int ClosestIndex(float current, float maxSize)
    {
        float percentage = current / maxSize;
        int index = (int) Mathf.Floor(percentage * eyeList.Length);
        return index;
    }
}
