// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public bool IsLocked = false;
    public Sprite LockedSprite;
    public Sprite UnlockedSprite;
    public string SceneToLoad;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (IsLocked)
        {
            spriteRenderer.sprite = LockedSprite;
        }
        else
        {
            spriteRenderer.sprite = UnlockedSprite;
        }
    }

    private void Start()
    {
        AsyncSceneLoader.GetInstance().LoadScene(SceneToLoad, false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsLocked && collision.tag == "Player")
        {
            AsyncSceneLoader.GetInstance().SwitchToNextScene();
        }
    }

    public void Unlock()
    {
        IsLocked = false;
        spriteRenderer.sprite = UnlockedSprite;
    }

    public void Lock()
    {
        IsLocked = true;
        spriteRenderer.sprite = LockedSprite;
    }
}
