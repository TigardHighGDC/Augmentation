// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLock : MonoBehaviour
{
    private Door door;
    private void Start()
    {
        door = GetComponent<Door>();
    }

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length != 0)
        {
            door.Lock();
        }
        else
        {
            door.Unlock();
        }
    }
}
