using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZipBomb : MonoBehaviour
{
    private ItemType itemType;

    private void Start()
    {
        itemType = GetComponent<ItemType>();
        PlayerHealth.CanDie = false;
        PlayerHealth.Invincibility = true;
        ItemHandling.PlayerHit += RandomDeath;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        PlayerHealth playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        playerHealth.Health = PlayerHealth.MaxHealth;

        if (itemType.DestroyItem)
        {
            PlayerHealth.Invincibility = false;
            ItemHandling.PlayerHit -= RandomDeath;
            PlayerHealth.CanDie = true;
            Destroy(gameObject);
        }
    }

    private void RandomDeath(float empty)
    {
        int random = Random.Range(0, 101);
        if (random <= 1)
        {
            PlayerHealth playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
            playerHealth.Death();
        }
    }
}