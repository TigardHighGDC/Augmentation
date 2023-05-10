// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionSet : MonoBehaviour
{
    public float corruptionAdd;
    public float corruptionIncreaseInterval;

    private CorruptionLevel corruption;

    private void Start()
    {
        CorruptionLevel.Add(corruptionAdd);
    }

    private void Update()
    {
        CorruptionLevel.Add(corruptionIncreaseInterval * Time.deltaTime);
    }
}
