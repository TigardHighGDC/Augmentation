// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncSceneLoader : MonoBehaviour
{
    public bool IsLoaded { get; private set; }

    private AsyncOperation asyncLoad;
    private bool activateOnLoad;

    private bool isInitialized = false;

    public void Initialize(string sceneName, bool activateOnLoad = true)
    {
        this.activateOnLoad = activateOnLoad;

        StartCoroutine(LoadScene(sceneName));
        isInitialized = true;
    }

    public void ActivateScene()
    {
        if (!isInitialized)
        {
            Assert.Boolean(false, "AsyncSceneLoader not initialized.");
            return;
        }

        asyncLoad.allowSceneActivation = true;
    }

    private IEnumerator LoadScene(string sceneName)
    {
        asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        IsLoaded = true;

        if (activateOnLoad)
        {
            ActivateScene();
        }
    }
}
