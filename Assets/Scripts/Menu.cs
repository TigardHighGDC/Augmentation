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

    private void Start()
    {
        if (PlayerPrefs.HasKey("Map"))
        {
            PlayerPrefs.DeleteKey("Map");
        }

        AsyncSceneLoader.GetInstance().LoadScene(Room, false);
    }

    public void OnStartButton()
    {
        AsyncSceneLoader.GetInstance().SwitchToNextScene();
    }
}
