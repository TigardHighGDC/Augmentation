// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject DialoguePanel;
    public TextMeshProUGUI DialogueText;
    public TextMeshProUGUI SpeakerText;
    public GameObject PortraitImage;

    public List<DialogueProfile> DialogueProfiles;

    [Range(0.01f, 0.1f)]
    public float TextSpeed = 0.04f;

    [HideInInspector]
    public bool DialogueIsActive { get; private set; }

    private Animator layoutAnimator;

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

    private const string speakerTag = "speaker";
    private const string layoutTag = "layout";

    private void Awake()
    {
        if (instance != null)
        {
            Assert.Boolean(false, "More than one DialogueManager in scene!");
        }

        DialoguePanel.SetActive(false);
        DialogueIsActive = false;
        layoutAnimator = DialoguePanel.GetComponent<Animator>();

        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
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
        PauseMenu.GameIsPaused = true;
        currentStory = new Story(inkJSON.text);
        DialogueIsActive = true;
        DialoguePanel.SetActive(true);

        // Set things to their defaults.
        SpeakerText.text = "";
        PortraitImage.GetComponent<Image>().sprite = null;
        SpeakerText.text = "????";
        layoutAnimator.Play("right");

        ContinueStory();
    }

    public void ExitDialogue()
    {
        PauseMenu.GameIsPaused = false;
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
            HandleTags(currentStory.currentTags);
        }
        else
        {
            ExitDialogue();
        }
    }

    private void HandleTags(List<string> tags)
    {
        foreach (string tag in tags)
        {
            string[] splitTag = tag.Split(':');
            Assert.Boolean(splitTag.Length == 2, "Invalid tag: " + tag);

            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case speakerTag:
                    DialogueProfile profile = DialogueProfiles.Find(x => x.Name == tagValue);
                    Assert.Boolean(profile != null, "Invalid speaker: " + tagValue);
                    SpeakerText.text = profile.Name;
                    PortraitImage.GetComponent<Image>().sprite = profile.Portrait;
                    break;
                case layoutTag:
                    layoutAnimator.Play(tagValue);
                    break;
                default:
                    Assert.Boolean(false, "Invalid tag: " + tag);
                    break; // Will never reach.
            }
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
