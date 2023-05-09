// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class Menu : MonoBehaviour
{
    public string Room;

    public void OnStartButton()
    {
        if (PlayerPrefs.HasKey("Map"))
        {
            PlayerPrefs.DeleteKey("Map");
        }

        // TODO: Use static scene manager. See GH-139.
        SceneManager.LoadScene(Room);
    }
}
