// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using Malee.List;
using UnityEngine;
using UnityEditor;

public class WeaponInventory : MonoBehaviour
{
    public List<WeaponData> AvailableWeapons;
    public int MaxWeapons = 3;
    public GameObject Pickupable;

    public List<WeaponData> NewWeapons;
    private static List<WeaponData> Weapons = new List<WeaponData>();

    private Gun playerGun;
    private bool hasRun = false;
    private int currentWeaponIndex = -1;
    private double lastWeaponSwitchTime = 0.0;
    private double allowedWeaponSwitchTime = 0.25;
    private static List<int> weaponAmmoAmounts = new List<int>();
    private static List<GameObject> weaponItems = new List<GameObject>();

    // clang-format off
    private Dictionary<int, KeyCode> weaponSwitchMap = new Dictionary<int, KeyCode> { 
        { 0, KeyCode.Alpha1 }, 
        { 1, KeyCode.Alpha2 }, 
        { 2, KeyCode.Alpha3 },
        { 3, KeyCode.Alpha4 }, 
        { 4, KeyCode.Alpha5 }, 
        { 5, KeyCode.Alpha6 },
        { 6, KeyCode.Alpha7 }, 
        { 7, KeyCode.Alpha8 }, 
        { 8, KeyCode.Alpha9 },
        { 9, KeyCode.Alpha0 } 
    };
    // clang-format on

    private void Start()
    {
        playerGun = GetComponent<Gun>();
        PlayerStats playerStats = PlayerStatManager.Instance.PlayerStats;

        if (playerStats.StartingWeapons.Count > 0 && !hasRun)
        {
            hasRun = true;
            Reset();

            foreach (string weaponName in playerStats.StartingWeapons)
            {
                WeaponData weapon = AvailableWeapons.Find(w => w.WeaponName == weaponName);
                Weapons.Add(weapon);
                weaponAmmoAmounts.Add(weapon.AmmoCapacity);
                SelectItem();
            }
        }
        else
        {
            WeaponData pistol = AvailableWeapons.Find(w => w.WeaponName == "Pistol");
            Weapons.Add(pistol);
            weaponAmmoAmounts.Add(pistol.AmmoCapacity);
            SelectItem();
        }

        if (Weapons.Count > 0)
        {
            ChangeWeapon(0);
        }
    }

    private void Update()
    {
        if (PauseMenu.GameIsPaused)
        {
            return;
        }

        if (Weapons.Count < 1 || Time.time - lastWeaponSwitchTime < allowedWeaponSwitchTime)
        {
            return;
        }

        if (ItemStorage.Weapon == null && CorruptionLevel.currentCorruption >= 50.0f)
        {
            ItemStorage.Weapon = ItemStorage.ReplaceItem(weaponItems[currentWeaponIndex]);
        }
        else if (ItemStorage.Weapon != null && CorruptionLevel.currentCorruption < 50.0f)
        {
            ItemStorage.Weapon = ItemStorage.DeleteItem(ItemStorage.Weapon);
        }

        // Switch weapons with keybinds.
        for (int i = 0; i < Weapons.Count; i++)
        {
            if (Input.GetKeyDown(weaponSwitchMap[i]))
            {
                ChangeWeapon(i);
                lastWeaponSwitchTime = Time.time;
                return;
            }
        }

        // Switch weapons with scroll wheel.
        if (Input.GetAxis("Mouse ScrollWheel") > 0.0f)
        {
            // Wrap around to the first weapon if we're at the end of the list.
            if (currentWeaponIndex == Weapons.Count - 1)
            {
                ChangeWeapon(0);
            }
            else
            {
                ChangeWeapon(currentWeaponIndex + 1);
            }

            lastWeaponSwitchTime = Time.time;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0.0f)
        {
            if (currentWeaponIndex == 0)
            {
                ChangeWeapon(Weapons.Count - 1);
            }
            else
            {
                ChangeWeapon(currentWeaponIndex - 1);
            }

            lastWeaponSwitchTime = Time.time;
        }

        // Drop weapon.
        if (Input.GetKeyDown(KeyCode.Q) && !playerGun.reloading && Weapons.Count > 1)
        {
            RemoveWeapon(currentWeaponIndex);
        }
    }

    public void Reset()
    {
        Weapons.Clear();
        weaponAmmoAmounts.Clear();
        weaponItems.Clear();
        currentWeaponIndex = -1;
    }

    private void ChangeWeapon(int newWeaponIndex, bool force = false, bool removed = false)
    {
        if (Weapons.Count < 1 || playerGun.reloading)
        {
            return;
        }
        else if (newWeaponIndex == currentWeaponIndex && !force)
        {
            return;
        }

        Assert.Boolean(newWeaponIndex < Weapons.Count);

        Gun gun = GetComponent<Gun>();

        if (currentWeaponIndex != -1 && Weapons.Count > currentWeaponIndex && !removed)
        {
            weaponAmmoAmounts[currentWeaponIndex] = gun.AmmoAmount;
        }

        if (CorruptionLevel.currentCorruption >= 50.0f)
        {
            ItemStorage.Weapon = ItemStorage.ReplaceItem(weaponItems[newWeaponIndex], ItemStorage.Weapon);
        }

        gun.Data = Weapons[newWeaponIndex];
        gun.AmmoAmount = weaponAmmoAmounts[newWeaponIndex];
        currentWeaponIndex = newWeaponIndex;
    }

    public void AddWeapon(WeaponData weapon, GameObject effect = null)
    {
        if (Weapons.Count < MaxWeapons)
        {
            Weapons.Add(weapon);
            weaponAmmoAmounts.Add(weapon.AmmoCapacity);

            if (effect == null)
            {
                SelectItem();
            }
            else
            {
                weaponItems.Add(effect);
            }
        }
        else
        {
            // Show the player that they must drop a weapon first.
            Debug.Log("Inventory is full"); // TODO: Remove debug log
        }
    }

    private void RemoveWeapon(int weaponIndex)
    {
        if (Weapons.Count <= 1)
        {
            return;
        }

        Assert.Boolean(weaponIndex == currentWeaponIndex);

        PickupableItem dropData = Instantiate(Pickupable, gameObject.transform.position, new Quaternion(0, 0, 0, 0))
                                      .GetComponent<PickupableItem>();
        dropData.Weapon = Weapons[weaponIndex];
        dropData.WeaponEffect = weaponItems[weaponIndex];

        weaponItems.RemoveAt(weaponIndex);
        Weapons.RemoveAt(weaponIndex);
        weaponAmmoAmounts.RemoveAt(weaponIndex);
        ChangeWeapon(0, true, true);
    }

    private void SelectItem()
    {
        GameObject[] choices = Resources.LoadAll<GameObject>("Item/Weapon");
        int random = Random.Range(0, choices.Length);
        weaponItems.Add(choices[random]);
    }
}
