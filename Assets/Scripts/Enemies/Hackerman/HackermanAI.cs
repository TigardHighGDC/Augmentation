using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackermanAI : EnemyAI
{
    public EnemyProjectileData Data;
    private bool canShoot = true;

    private void Update()
    {
        if (canShoot)
        {
            StartCoroutine(BeginFiring());
        }
        
    }

    private IEnumerator BeginFiring()
    {
        canShoot = false;
        Fire();
        yield return new WaitForSeconds(Data.BulletPerSecond);
        canShoot = true;
    }
}
