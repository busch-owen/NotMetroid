using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Weapons Stats", menuName = "AGOS/Stats/EnemyWeaponStats", order = 1)]
public class EnemyWeaponStats : ScriptableObject
{
    [field: SerializeField] public float Damage {get; private set;}
    [field: SerializeField] public float FireForce {get; private set;}
    [field: SerializeField] public float FireRate {get; private set;}
    [field: SerializeField] public int ProjectileLimit { get; private set; }
    [field: SerializeField] public int FiringCooldown { get; private set; }
    
    [field: SerializeField] public Projectile Projectile { get; private set; }
}