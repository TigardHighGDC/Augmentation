// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    public Camera Camera;

    private void Update()
    {
        var mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
        var relativePoint = transform.position - mousePosition;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(relativePoint.y, relativePoint.x) * Mathf.Rad2Deg + 90);
    }
}
