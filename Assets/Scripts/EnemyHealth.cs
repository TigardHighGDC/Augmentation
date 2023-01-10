using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float maxTimer;
    private float timer;


    void Start()
    {
        timer = maxTimer;
    }


    void Update()
    {
        if(timer <= maxTimer && timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }


    public void Damage(float damageAmount)
    {
        if (timer <= 0)
        {
            health = health - damageAmount;
            timer = maxTimer;
        }
    }
}
