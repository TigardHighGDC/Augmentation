using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnexplainableBug : MonoBehaviour
{	
    private ItemType itemType;

    private void Start()
    {
        itemType = GetComponent<ItemType>();
        ItemHandling.PlayerHit += DealDamageToCorruption;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        PlayerHealth playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        playerHealth.Health = (CorruptionLevel.currentCorruption / CorruptionLevel.currentCorruption) * PlayerHealth.MaxHealth;

        if (itemType.DestroyItem)
        {
            ItemHandling.PlayerHit -= DealDamageToCorruption;
            Destroy(gameObject);
        }
    }

    private void DealDamageToCorruption(float damage)
    {
        CorruptionLevel.currentCorruption -= damage;
    }
}