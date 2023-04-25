using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponEffectUI : MonoBehaviour
{
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if (CorruptionLevel.currentCorruption >= 50.0f && ItemStorage.Weapon != null)
        {
            image.sprite = ItemStorage.Weapon.GetComponent<ItemType>().Image;
            image.enabled = true;
        }
        else
        {
            image.enabled = false;
            image.sprite = null;
        }
    }
}
