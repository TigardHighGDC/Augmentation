// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject VisualCue;
    public TextAsset InkJSON;

    private bool playerInRange;
    private bool activatedThisTrigger;

    private void Awake()
    {
        playerInRange = false;
        activatedThisTrigger = false;
        VisualCue.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange)
        {
            VisualCue.SetActive(true);

            if (!DialogueManager.GetInstance().DialogueIsActive && !activatedThisTrigger)
            {
                DialogueManager.GetInstance().EnterDialogue(InkJSON);
                activatedThisTrigger = true;
            }
        }
        else
        {
            VisualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            activatedThisTrigger = false;
            DialogueManager.GetInstance().ExitDialogue();
        }
    }
}
