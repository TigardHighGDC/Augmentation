// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static float MaxHealth = 150f;
    public static bool CanDie = true;
    public static bool Invincibility = false;

    [HideInInspector]
    public float Health;
    public SliderBarScript sliderBar;

    private float remainingInvincibilityTime;
    private bool dying = false;

    private void Update()
    {
        // Changes the max health encase of an item change
        sliderBar.SetMaxHealth(MaxHealth);
        sliderBar.SetHealth(Health);
        Health = Mathf.Min(MaxHealth, Health);
    }

    public void Damage(float damageAmount)
    {
        ItemHandling.PlayerHit?.Invoke(damageAmount);

        if (!Invincibility)
        {
            Health -= damageAmount;
            sliderBar.SetHealth(Health);
        }

        if (Health <= 0 && !dying && CanDie)
        {
            dying = true;
            Death();
        }
    }

    // Handles changes when the player dies
    public void Death()
    {
        AsyncSceneLoader.GetInstance().Unload();
        SceneManager.LoadScene("MainMenu");
    }
}
