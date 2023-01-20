// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string SceneName; // Scene that the door will transition to.

    // Player enters the door.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered door.");
            // TODO: May want to add a button press to enter the door.
            StartCoroutine(LoadSceneAsync());
        }
    }

    private IEnumerator LoadSceneAsync() {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneName);

        // Wait until the asynchronous scene fully loads.
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
