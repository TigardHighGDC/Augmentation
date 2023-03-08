// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class SceneLoader : MonoBehaviour
{
    public string SceneName;

    public IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
