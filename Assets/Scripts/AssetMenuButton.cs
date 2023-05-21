// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using UnityEngine;

public class AssetMenuButton : MonoBehaviour
{
    private void Update()
    {
        if (!PlayerPrefs.HasKey("PlayerStats"))
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
