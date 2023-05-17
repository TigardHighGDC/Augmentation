using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceGun : MonoBehaviour
{
    private ItemType itemType;

    private void Start()
    {

        itemType = GetComponent<ItemType>();
        DontDestroyOnLoad(gameObject);
        ItemHandling.PlayerGunReload += DiceGunEvent;
    }

    private void Update()
    {
        if (itemType.DestroyItem)
        {
            ItemHandling.PlayerGunReload -= DiceGunEvent;
            Destroy(gameObject);
        }
    }

    private void DiceGunEvent(GameObject player)
    {
        Gun playerGun = player.GetComponent<Gun>();
        int maxAmmo = playerGun.Data.AmmoCapacity * Gun.C_AmmoCapacity;
        int random = Random.Range(0, maxAmmo * 2 + 1);
        playerGun.AmmoAmount = random;
    }
}