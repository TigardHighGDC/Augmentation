// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemDrop : NonPlayerHealth
{
    public List<GameObject> SpawnPool;

    public override void Death()
    {
        // Random item dropped from the public list.
        int randomItem = Random.Range(0, SpawnPool.Count);
        GameObject toSpawn = SpawnPool[randomItem];

        Instantiate(toSpawn, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
