using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Projectile", menuName = "ScriptableObject/EnemyProjectileData")]
public class EnemyProjectileData : ScriptableObject
{
    [Header("Stats")]
    public float Damage;
    public float BulletSpeed;
    public float BulletPerSecond;
}
