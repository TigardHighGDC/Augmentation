using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhaseManager : MonoBehaviour
{
    public static Phases CurrentPhase;
    public static Phases PreviousPhase;

    private EnemyHealth enemyHealth;
    private BossBulletPattern BossBullets;
    private bool ShouldDestroyBullets = false;

    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        BossBullets = GetComponent<BossBulletPattern>();
        SetPhase();
    }

    private void Update()
    {
        PreviousPhase = CurrentPhase;
        SetPhase();

        if (PreviousPhase != CurrentPhase)
        {
            ShouldDestroyBullets = true;
        }

        if (BossBullets.IsAttacking)
        {
            return;
        }

        if (ShouldDestroyBullets)
        {
            DestroyAllBullets();
            ShouldDestroyBullets = false;
        }

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

    private void SetPhase(float healthChunck = 1500.0f, float addedEndHealth = 2500f)
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
}
