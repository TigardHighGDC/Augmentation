// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public string Room;

    public void OnStartButton()
    {
        // TODO: Use static scene manager. See GH-139.
        SceneManager.LoadScene(Room);
    }
}
