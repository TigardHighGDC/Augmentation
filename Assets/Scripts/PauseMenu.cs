// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    public void Options()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }

    public void Back()
    {
        pauseMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1.0f;
        // TODO: Use static scene manager. See GH-139.
        SceneManager.LoadScene("MainMenu");
    }
}
