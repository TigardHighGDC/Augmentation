// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class Menu : MonoBehaviour
{
    public void OnNewGameButton()
    {
        ResetGameData();

        PlayerStatManager.Instance.UpdateState();
        // PlayerStatManager.Instance.UpdatePlayerState();

        AsyncSceneLoader.GetInstance().LoadScene("Level Map");
    }

    public void OnContinueButton()
    {
        // PlayerStatManager.Instance.UpdatePlayerState();
        PlayerStatManager.Instance.UpdateState();
        AsyncSceneLoader.GetInstance().LoadScene("Level Map");
    }

    public void OnAssetStoreButton()
    {
        AsyncSceneLoader.GetInstance().LoadScene("AssetStore");
    }

    public void OnResetButton()
    {
        ResetGameData();
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    private void ResetGameData()
    {
        PlayerPrefs.DeleteKey("PlayerStats");
        PlayerPrefs.DeleteKey("Map");
    }
}
