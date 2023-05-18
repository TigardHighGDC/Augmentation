// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDialogueManager : MonoBehaviour
{
    public TextAsset IgnoringText;
    public TextAsset DenialText;
    public TextAsset AngerText;
    public TextAsset BargainingText;
    public TextAsset DepressionText;
    public TextAsset AcceptanceText;

    private TextAsset InkJSON;
    private bool activatedThisTrigger = false;

    public void BeginBossDialogue()
    {
        if (!DialogueManager.GetInstance().DialogueIsActive && !DialogueManager.GetInstance().DialogueIsActive)
        {
            SetInkJSONPhase();
            DialogueManager.GetInstance().EnterDialogue(InkJSON);
        }
    }

    private void SetInkJSONPhase()
    {
        switch (BossPhaseManager.CurrentPhase)
        {
            case BossPhaseManager.Phases.Ignoring:
                InkJSON = IgnoringText;
                break;
            case BossPhaseManager.Phases.Denial:
                InkJSON = DenialText;
                break;
            case BossPhaseManager.Phases.Anger:
                InkJSON = AngerText;
                break;
            case BossPhaseManager.Phases.Bargaining:
                InkJSON = BargainingText;
                break;
            case BossPhaseManager.Phases.Depression:
                InkJSON = DepressionText;
                break;
            case BossPhaseManager.Phases.Acceptance:
                InkJSON = AcceptanceText;
                break;
        }
    }
}
