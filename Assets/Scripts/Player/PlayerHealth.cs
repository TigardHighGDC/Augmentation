// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static float MaxHealth = 150f;
    [HideInInspector] public float Health;
    public float InvincibilityTimer;
    public SliderBarScript sliderBar;

    private float remainingInvincibilityTime;

    // set timer to timer :P
    private void Start()
    {
        remainingInvincibilityTime = InvincibilityTimer;
        Health = MaxHealth;
        sliderBar.SetMaxHealth(MaxHealth);
        sliderBar.SetHealth(Health);
    }

    private void Update()
    {
        // Update remaining invincibility time
        if (remainingInvincibilityTime <= InvincibilityTimer && remainingInvincibilityTime > 0)
        {
            remainingInvincibilityTime -= Time.deltaTime;
        }

        // Changes the max health encase of an item change
        sliderBar.SetMaxHealth(MaxHealth);
        Health = Mathf.Min(MaxHealth, Health);
    }

    public void Damage(float damageAmount)
    {
        if (remainingInvincibilityTime <= 0)
        {
            Health -= damageAmount;
            remainingInvincibilityTime = InvincibilityTimer;
            sliderBar.SetHealth(Health);
        }

        if (Health <= 0)
        {
            Death();
        }
    }

    // Handles changes when the player dies
    private void Death()
    {
        AsyncSceneLoader.GetInstance().LoadScene("MainMenu");
    }
}
