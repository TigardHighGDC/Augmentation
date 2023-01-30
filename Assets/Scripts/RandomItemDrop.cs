// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemDrop : MonoBehaviour
{
    public List<GameObject> SpawnPool;

    private NonPlayerHealth potHealth;

    private void Start()
    {
        potHealth = GetComponent<NonPlayerHealth>();
    }

    private void Update()
    {
        // Zero health makes it "break".
        if (potHealth.Health <= 0)
        {
            GameObject toSpawn;
            int randomItem = 0;

            // Random item dropped from the public list.
            randomItem = Random.Range(0, SpawnPool.Count);
            toSpawn = SpawnPool[randomItem];

            Instantiate(toSpawn, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void Damage(float damageAmount)
    {
        potHealth.Health = potHealth.Health - damageAmount;
    }
}
