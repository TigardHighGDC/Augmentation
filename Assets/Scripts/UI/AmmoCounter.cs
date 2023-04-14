// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoCounter : MonoBehaviour
{
    public TextMeshProUGUI AmmoText;

    public void Text(WeaponData data, int ammoAmount)
    {
        AmmoText.text = ammoAmount.ToString() + " | " + data.AmmoCapacity.ToString();
    }
}
