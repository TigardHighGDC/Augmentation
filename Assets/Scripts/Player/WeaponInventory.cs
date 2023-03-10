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

            AddWeapon(pistol);
            ChangeWeapon(0);
        }
        else if (startingLoadout == StartingLoadout.ALL)
        {
            // Add all weapons to inventory.
            // All being a relative term as this is only for testing.
            WeaponData pistol = AssetDatabase.LoadAssetAtPath<WeaponData>("Assets/Scripts/Weapons/Data/Pistol.asset");
            WeaponData shotgun = AssetDatabase.LoadAssetAtPath<WeaponData>("Assets/Scripts/Weapons/Data/Shotgun.asset");
            WeaponData sniper = AssetDatabase.LoadAssetAtPath<WeaponData>("Assets/Scripts/Weapons/Data/Sniper.asset");

            AddWeapon(pistol);
            AddWeapon(shotgun);
            AddWeapon(sniper);
            ChangeWeapon(0);
        }
        else
        {
            state = State.NO_WEAPONS;
        }
    }

    private void Update()
    {
        if (state == State.NO_WEAPONS)
        {
            return;
        }

        // Switch weapons.
        for (int i = 0; i < Weapons.Count; i++)
        {
            if (Input.GetKeyDown(weaponSwitchMap[i]))
            {
                ChangeWeapon(i);
                return;
            }
        }

        // Drop weapon.
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RemoveWeapon(currentWeaponIndex);
        }
    }

    private void ChangeWeapon(int newWeaponIndex)
    {
        if (newWeaponIndex == currentWeaponIndex || state == State.NO_WEAPONS)
        {
            return;
        }

        Assert.Boolean(newWeaponIndex < Weapons.Count);

        Debug.Log("Changing weapon to " + Weapons[newWeaponIndex].name); // TODO: Remove debug log

        Gun gun = GetComponent<Gun>();
        gun.Data = Weapons[newWeaponIndex];
        currentWeaponIndex = newWeaponIndex;
    }

    public void AddWeapon(WeaponData weapon)
    {
        if (state == State.NO_WEAPONS || state == State.HAS_WEAPONS)
        {
            Weapons.Add(weapon);

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
        else if (weaponIndex == 0)
        {
            Debug.Log("Cannot drop the starting weapon"); // TODO: Remove debug log
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
        ChangeWeapon(0);
    }
}
