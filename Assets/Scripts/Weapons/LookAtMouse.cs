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
        if(!PauseMenu.GameIsPaused)
        {
            Vector3 mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 relativePoint = transform.position - mousePosition;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(relativePoint.y, relativePoint.x) * Mathf.Rad2Deg + 90);
        }
    }
}
