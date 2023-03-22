// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assert
{
    public static void Boolean(bool condition, string message = "")
    {
        if (!condition)
        {
            Debug.LogError(message);
        }
    }
}
