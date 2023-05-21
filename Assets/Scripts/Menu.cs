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

    public void OnTutorialButton()
    {
        AsyncSceneLoader.GetInstance().LoadScene("Tutorial");
    }

    private void ResetGameData()
    {
        PlayerPrefs.DeleteKey("PlayerStats");
        PlayerPrefs.DeleteKey("Map");

        GameObject[] itemUiElements = GameObject.FindGameObjectsWithTag("Item UI");

        foreach (GameObject itemUiElement in itemUiElements)
        {
            Destroy(itemUiElement);
        }

        WeaponInventory.hasRun = false;
    }
}
