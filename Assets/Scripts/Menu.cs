// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public string Room;

    private void Start()
    {
        AsyncSceneLoader.GetInstance().LoadScene(Room, false);
    }

    public void OnStartButton()
    {
        AsyncSceneLoader.GetInstance().SwitchToNextScene();
    }
}
