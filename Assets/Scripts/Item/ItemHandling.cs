// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandling : MonoBehaviour
{
    public delegate void BasicEventHandler();

    public delegate void GameObjectEventHandler(GameObject value);

    public delegate void FloatEventHandler(float value);

    public static BasicEventHandler BulletHit;

    public static GameObjectEventHandler PlayerGunReload;

    public static FloatEventHandler PlayerHit;
}
