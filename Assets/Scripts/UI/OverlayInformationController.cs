using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OverlayInformationController : MonoBehaviour
{
    public EffectType Effect;

    private GameObject EffectGameObject;

    private void Update()
    {
        SelectEffect();
        if (Input.GetKey(KeyCode.I) && EffectGameObject != null && !PauseMenu.GameIsPaused)
        {
            EnableText(true);
            GetSetChildText();
            GetComponent<Image>().enabled = true;
        }
        else
        {
            EnableText(false);
            GetComponent<Image>().enabled = false;
        }
    }

    private void EnableText(bool enable)
    {
        TextMeshProUGUI childText = GetComponentsInChildren<TextMeshProUGUI>()[0];
        childText.enabled = enable;
    }

    private void GetSetChildText()
    {
        TextMeshProUGUI childText = GetComponentsInChildren<TextMeshProUGUI>()[0];
        ItemType itemInformation = EffectGameObject.GetComponent<ItemType>();
        childText.text = $"{itemInformation.Name}: {itemInformation.ItemStats}";
    }

    private void SelectEffect()
    {
        switch (Effect)
        {
        case EffectType.Fragmentation:
            EffectGameObject = ItemStorage.Fragmentation;
            break;
        case EffectType.Weapon:
            EffectGameObject = ItemStorage.Weapon;
            break;
        }
    }

    public enum EffectType
    {
        Fragmentation,
        Weapon
    }
}
