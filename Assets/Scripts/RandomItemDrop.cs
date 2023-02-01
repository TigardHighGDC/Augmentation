// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemDrop : NonPlayerHealth
{
    public List<GameObject> SpawnPool;

    private NonPlayerHealth potHealth;

    private void Start()
    {
        potHealth = GetComponent<NonPlayerHealth>();
    }

    public override void Death()
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
