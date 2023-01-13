using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxTimer;
    private float timer;

    // set timer to timer :P
    void Start()
    {
        timer = maxTimer;
    }

    // Timer for a little invicability before taking damage again
    void Update()
    {
        if (timer <= maxTimer && timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    // Taking damage
    public void Damage(float damageAmount)
    {
        if (timer <= 0)
        {
            health = health - damageAmount;
            timer = maxTimer;
        }
    }
}
