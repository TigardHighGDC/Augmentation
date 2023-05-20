using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CorruptionCountShower : MonoBehaviour
{
    private void Update()
    {
        GetComponent<TextMeshProUGUI>().text = $"{CorruptionLevel.currentCorruption}";
    }
}
