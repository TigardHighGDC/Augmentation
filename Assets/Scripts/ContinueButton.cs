// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    private void Update()
    {
        if (!PlayerPrefs.HasKey("Map") && !PlayerPrefs.HasKey("PlayerStats"))
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
