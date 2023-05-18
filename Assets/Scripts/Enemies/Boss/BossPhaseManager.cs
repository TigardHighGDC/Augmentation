// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhaseManager : MonoBehaviour
{
    public static Phases CurrentPhase;
    public static Phases PreviousPhase;

    private BossDialogueManager bossDialogueManager;
    private EnemyHealth enemyHealth;
    private BossBulletPattern BossBullets;
    private bool shouldDestroyBullets = false;

    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        BossBullets = GetComponent<BossBulletPattern>();
        bossDialogueManager = GetComponent<BossDialogueManager>();
        SetPhase();
        bossDialogueManager.BeginBossDialogue();
    }

    private void Update()
    {
        PreviousPhase = CurrentPhase;
        SetPhase();

        if (PreviousPhase != CurrentPhase)
        {
            shouldDestroyBullets = true;
        }

        if (BossBullets.IsAttacking)
        {
            return;
        }

        if (shouldDestroyBullets && !DialogueManager.GetInstance().DialogueIsActive)
        {
            DestroyAllBullets();
            bossDialogueManager.BeginBossDialogue();
            shouldDestroyBullets = false;
        }

        if (DialogueManager.GetInstance().DialogueIsActive)
        {
            return;
        }

        // Spawns bullets based off of boss phase
        switch (CurrentPhase)
        {
            case Phases.Ignoring:
                StartCoroutine(BossBullets.IgnoringPhase());
                break;
            case Phases.Denial:
                StartCoroutine(BossBullets.IgnoringPhase());
                BossBullets.DenialPhase();
                break;
            case Phases.Anger:
                StartCoroutine(BossBullets.AngerPhase(Random.Range(-10, 6)));
                break;
            case Phases.Bargaining:
                StartCoroutine(BossBullets.BargainingPhase());
                break;
            case Phases.Depression:
                StartCoroutine(BossBullets.DepressionPhase());
                BossBullets.DepressionPhaseSecondaryAttack();
                break;
            case Phases.Acceptance:
                StartCoroutine(BossBullets.AcceptancePhase());
                break;
        }
    }

    private void SetPhase(float healthChunck = 1500.0f, float addedEndHealth = 2500.0f)
    {
        int phaseIndex = (int)Mathf.Ceil((enemyHealth.Health - addedEndHealth) / healthChunck);

        switch (phaseIndex)
        {
            case 5:
                CurrentPhase = Phases.Ignoring;
                break;
            case 4:
                CurrentPhase = Phases.Denial;
                break;
            case 3:
                CurrentPhase = Phases.Anger;
                break;
            case 2:
                CurrentPhase = Phases.Bargaining;
                break;
            case 1:
                CurrentPhase = Phases.Depression;
                break;
            default:
                CurrentPhase = Phases.Acceptance;
                break;
        }
    }

    private void DestroyAllBullets()
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");

        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
    }

    public enum Phases
    {
        Ignoring,
        Denial,
        Anger,
        Bargaining,
        Depression,
        Acceptance
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Bullets during the acceptance phase damages the boss
        if (CurrentPhase != Phases.Acceptance)
        {
            return;
        }

        if (collider.gameObject.TryGetComponent<EnemyBullet>(out EnemyBullet bullet))
        {
            enemyHealth.Health -= 15.0f;
            Destroy(collider.gameObject);
        }
    }
}
