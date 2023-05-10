// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncSceneLoader : MonoBehaviour
{
    private static AsyncSceneLoader instance;

    private string nextSceneName;
    private AsyncOperation asyncLoad;

    private void Awake()
    {
        if (instance != null)
        {
            Assert.Boolean(false, "AsyncSceneLoader already exists. There can only be one AsyncSceneLoader.");
        }

        instance = this;
    }

    public static AsyncSceneLoader GetInstance()
    {
        return instance;
    }

    public void LoadScene(string sceneName, bool switchImmediately = true)
    {
        nextSceneName = sceneName;
        StartCoroutine(LoadSceneAsync(switchImmediately));
    }

    public void SwitchToNextScene()
    {
        asyncLoad.allowSceneActivation = true;
    }

    private IEnumerator LoadSceneAsync(bool switchImmediately)
    {
        asyncLoad = SceneManager.LoadSceneAsync(nextSceneName);

        if (!switchImmediately)
        {
            asyncLoad.allowSceneActivation = false;
        }

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
