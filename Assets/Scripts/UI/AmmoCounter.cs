using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoCounter : MonoBehaviour
{
    public TextMeshProUGUI bulletText;

    public void Text(WeaponData data, int ammoAmount)
    {
        bulletText.text = ammoAmount.ToString() + " | " + data.AmmoCapacity.ToString();
    }
}
