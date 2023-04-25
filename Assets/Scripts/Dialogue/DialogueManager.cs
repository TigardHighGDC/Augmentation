// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public GameObject DialoguePanel;
    public TextMeshProUGUI DialogueText;

    [Range(0.01f, 0.1f)]
    public float TextSpeed = 0.04f;

    [HideInInspector]
    public bool DialogueIsActive;

    private Story currentStory;
    private Coroutine displayLineCoroutine;
    private bool readyToContinue = false;
    private bool fillLine = false;

    private static DialogueManager instance;

    // clang-format off
    private static List<KeyCode> validContinueKeys = new List<KeyCode>() { 
        KeyCode.Space, 
        KeyCode.Return, 
        KeyCode.KeypadEnter 
    };
    // clang-format on

    private void Awake()
    {
        if (instance != null)
        {
            Assert.Boolean(false, "More than one DialogueManager in scene!");
        }

        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        DialoguePanel.SetActive(false);
        DialogueIsActive = false;
    }

    private void Update()
    {
        // Only update if dialogue is active.
        if (!DialogueIsActive)
        {
            return;
        }

        bool continuePressed = ContinuePressed();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitDialogue();
            return;
        }
        else if (continuePressed && readyToContinue)
        {
            ContinueStory();
        }
        else if (!readyToContinue && continuePressed)
        {
            fillLine = true;
        }
    }

    public void EnterDialogue(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        DialogueIsActive = true;
        DialoguePanel.SetActive(true);

        ContinueStory();
    }

    public void ExitDialogue()
    {
        if (displayLineCoroutine != null)
        {
            StopCoroutine(displayLineCoroutine);
            displayLineCoroutine = null;
        }

        DialogueIsActive = false;
        DialoguePanel.SetActive(false);
        DialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }

            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));
        }
        else
        {
            ExitDialogue();
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        DialogueText.maxVisibleCharacters = 0;
        DialogueText.text = line;

        readyToContinue = false;

        foreach (char letter in line.ToCharArray())
        {
            if (fillLine)
            {
                DialogueText.maxVisibleCharacters = line.Length;
                break;
            }

            DialogueText.maxVisibleCharacters++;
            yield return new WaitForSeconds(TextSpeed);
        }

        readyToContinue = true;
        fillLine = false;
    }

    private bool ContinuePressed()
    {
        foreach (KeyCode key in validContinueKeys)
        {
            if (Input.GetKeyDown(key))
            {
                return true;
            }
        }

        return false;
    }
}
