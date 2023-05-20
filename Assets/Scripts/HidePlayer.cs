// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePlayer : MonoBehaviour
{
    private bool hidden = false;

    private void Update()
    {
        if (!hidden)
        {
            // Find the player game object and set it to inactive
            GameObject player = GameObject.Find("Player 1");
            player.SetActive(false);
            hidden = true;
        }
    }
}
