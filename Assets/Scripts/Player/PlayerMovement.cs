// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed;

    private float x;
    private float y;
    private Rigidbody2D rb;
    private Vector2 direction;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        if (x != 0)
        {
            transform.localScale = new Vector3(-1 * x, transform.localScale[1], transform.localScale[2]);
        }

        direction = new Vector2(x, y);
        direction.Normalize();
        rb.velocity = direction * Speed;
    }
}
