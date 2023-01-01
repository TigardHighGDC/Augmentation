using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObject/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("Stats")]
    public float ReloadSpeed;
    public float BulletSpeed;
    public float BulletPerSecond;
    public int BulletPerTrigger;
    public int AmmoCapacity;
    public float Spread;
    public float Knockback;
}
