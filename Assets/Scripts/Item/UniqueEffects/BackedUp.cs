using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackedUp : MonoBehaviour
{
    private ItemType itemType;

    private void Start()
    {
        PlayerHealth.CanDie = false;
        itemType = GetComponent<ItemType>();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        PlayerHealth playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        if (playerHealth.Health <= 3.0f)
        {
            CorruptionLevel.FragmentationReset();
            CorruptionLevel.Add(10000f);
            playerHealth.Health = PlayerHealth.MaxHealth;

            PlayerHealth.CanDie = true;
            Destroy(gameObject);
        }

        if (itemType.DestroyItem)
        {
            PlayerHealth.CanDie = true;
            Destroy(gameObject);
        }
    }
}