// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObject/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("Stats")]
    public float Damage;
    public float Size;
    public float ReloadSpeed;
    public float BulletSpeed;
    public float CanShootInterval;
    public int BulletPerTrigger;
    public int AmmoCapacity;
    public float Spread;
    public float Knockback;
    public float DespawnTime;
    public bool AutoReload;

    [Header("SoundFX")]
    public AudioClip GunShotSound;
    public float GunShotVolume;
    public AudioClip ReloadSound;
    public float ReloadVolume;
}
