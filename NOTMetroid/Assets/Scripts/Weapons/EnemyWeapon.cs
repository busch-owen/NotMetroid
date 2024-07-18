using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private EnemyWeaponStats weaponStats;

    [SerializeField] private Vector3 fireOffset;

    private float _timeToNextFire;
    private PoolManager _poolManager;

    private Vector3 _firePos;
    
    protected virtual void Awake()
    {
        _poolManager = FindObjectOfType<PoolManager>();
    }

    public void Shoot(Vector2 fireDirection)
    {
        if (Time.time >= _timeToNextFire)
        {
            _timeToNextFire = Time.time + 1 / weaponStats.FireRate;
            
            var newProjectile = (Projectile)_poolManager.Spawn(weaponStats.Projectile.name);
            newProjectile.transform.position = (transform.position + fireOffset);
            _firePos = (transform.position + fireOffset) * fireDirection;
            newProjectile.RB.AddForce(fireDirection * weaponStats.FireForce);
        }
    }
}