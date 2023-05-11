using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimator : MonoBehaviour
{

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        switch (BossPhaseManager.CurrentPhase)
        {
            case BossPhaseManager.Phases.Ignoring:
                anim.Play("Ignoring");
                break;
            case BossPhaseManager.Phases.Denial:
                anim.Play("Denial");
                break;
            case BossPhaseManager.Phases.Anger:
                anim.Play("Anger");
                break;
            case BossPhaseManager.Phases.Bargaining:
                anim.Play("Bargaining");
                break;
            case BossPhaseManager.Phases.Depression:
                anim.Play("Depression");
                break;
            case BossPhaseManager.Phases.Acceptance:    
                anim.Play("Acceptance");
                break;
            default:
                break;
        }
    }
}
