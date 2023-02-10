using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : NonPlayerHealth
{
    [SerializeField]
    private float corruptionStrength;

    public override void Death()
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<CorruptionLevel>().Add(corruptionStrength);
        Destroy(gameObject);
    }
}