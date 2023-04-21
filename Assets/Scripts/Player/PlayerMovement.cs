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
    private Animator anim;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        PlayerMovementAnimation(x, y);

        direction = new Vector2(x, y);
        direction.Normalize();
        rb.velocity = direction * Speed * CorruptionLevel.PlayerSpeedIncrease;
    }

    private void PlayerMovementAnimation(float x, float y)
    {
        if (x != 0 || y != 0)
        {
            anim.speed = CorruptionLevel.PlayerSpeedIncrease;
            anim.Play("PlayerMove");
        }
        else
        {
            anim.Play("Idle");
        }
    }
}
