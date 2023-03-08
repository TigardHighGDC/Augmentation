// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeTracking : MonoBehaviour
{
    public float MaxEyeTravel;
    public float MaxDistance;

    private Transform body;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        body = transform.parent.parent;
    }

    private void Update()
    {
        transform.position = DistanceFromCenter(player.transform.position);
    }

    private Vector3 DistanceFromCenter(Vector3 target)
    {
        Vector3 travel = new Vector3(0, 0, 0);
        float position = target[0] - body.position[0];
        float sign = Mathf.Sign(position);

        // Sets max for distance that effects eye calculation
        float max = Mathf.Min(MaxDistance, Mathf.Abs(position));

        // Growth rate of square root create high movement at the beginning but slows at the end.
        travel[0] = body.position[0] + Mathf.Sqrt((4 / MaxDistance) * max) * sign * MaxEyeTravel;

        position = target[1] - body.position[1];
        sign = Mathf.Sign(position);
        max = Mathf.Min(MaxDistance, Mathf.Abs(position));
        travel[1] = body.position[1] + Mathf.Sqrt((4 / MaxDistance) * max) * sign * MaxEyeTravel;

        return travel;
    }
}
