// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WeaponInventory : MonoBehaviour
{
    public List<WeaponData> Weapons;
    public int MaxWeapons = 3;

    private int currentWeaponIndex = -1;
    private double lastWeaponSwitchTime = 0.0;
    private double allowedWeaponSwitchTime = 1.0;
    private Dictionary<string, int> weaponAmmoMap = new Map<String, int>();

    enum StartingLoadout
    {
        PISTOL,
        ALL
    }

    private StartingLoadout startingLoadout = StartingLoadout.ALL; // For testing purposes.

    enum State
    {
        NO_WEAPONS,
        HAS_WEAPONS,
        FULL_INVENTORY
    }

    private State state;

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
        state = State.NO_WEAPONS;

        if (Weapons.Count > 0)
        {
            // Starting weapons were custom set.
            state = State.HAS_WEAPONS;
            ChangeWeapon(0);
        }

        if (startingLoadout == StartingLoadout.PISTOL)
        {
            // Add pistol to inventory.
            WeaponData pistol = AssetDatabase.LoadAssetAtPath<WeaponData>("Assets/Scripts/Weapons/Data/Pistol.asset");

            AddWeapon(pistol, "Pistol");
            ChangeWeapon(0);
        }
        else if (startingLoadout == StartingLoadout.ALL)
        {
            // Add all weapons to inventory.
            // All being a relative term as this is only for testing.
            WeaponData pistol = AssetDatabase.LoadAssetAtPath<WeaponData>("Assets/Scripts/Weapons/Data/Pistol.asset");
            WeaponData shotgun = AssetDatabase.LoadAssetAtPath<WeaponData>("Assets/Scripts/Weapons/Data/Shotgun.asset");
            WeaponData sniper = AssetDatabase.LoadAssetAtPath<WeaponData>("Assets/Scripts/Weapons/Data/Sniper.asset");

            AddWeapon(pistol, "Pistol");
            AddWeapon(shotgun, "Shotgun");
            AddWeapon(sniper, "Sniper");
            ChangeWeapon(0);
        }
        else
        {
            state = State.NO_WEAPONS;
        }
    }

    private void Update()
    {
        if (state == State.NO_WEAPONS || Time.time - lastWeaponSwitchTime < allowedWeaponSwitchTime)
        {
            return;
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
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
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
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RemoveWeapon(currentWeaponIndex);
        }
    }

    private void ChangeWeapon(int newWeaponIndex, bool force = false)
    {
        if (state == State.NO_WEAPONS)
        {
            return;
        }
        else if (newWeaponIndex == currentWeaponIndex && !force)
        {
            return;
        }

        Assert.Boolean(newWeaponIndex < Weapons.Count);

        Debug.Log("Changing weapon to " + Weapons[newWeaponIndex].name); // TODO: Remove debug log

        Gun gun = GetComponent<Gun>();
        gun.Data = Weapons[newWeaponIndex];
        currentWeaponIndex = newWeaponIndex;
    }

    public void AddWeapon(WeaponData weapon, string weaponName)
    {
        if (state == State.NO_WEAPONS || state == State.HAS_WEAPONS)
        {
            Weapons.Add(weapon);
            weaponAmmoMap.Add(weaponName, weapon.MaxAmmo);

            if (state == State.NO_WEAPONS)
            {
                state = State.HAS_WEAPONS;
            }
            else if (state == State.HAS_WEAPONS && Weapons.Count == MaxWeapons)
            {
                state = State.FULL_INVENTORY;
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
        if (state == State.NO_WEAPONS)
        {
            return;
        }

        Assert.Boolean(weaponIndex == currentWeaponIndex);

        if (state == State.FULL_INVENTORY)
        {
            state = State.HAS_WEAPONS;
        }
        else if (state == State.HAS_WEAPONS && Weapons.Count == 1)
        {
            state = State.NO_WEAPONS;
        }

        Weapons.RemoveAt(weaponIndex);
        ChangeWeapon(0, true);
    }
}
