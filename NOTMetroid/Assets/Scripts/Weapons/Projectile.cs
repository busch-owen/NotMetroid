using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Projectile : PoolObject
{
    [SerializeField] private float projectileLifetime = 1f;
    [SerializeField] public float _damage;
    public Rigidbody2D RB { get; private set; }

    private void OnEnable()
    {
        RB = GetComponent<Rigidbody2D>();
        Invoke(nameof(OnDeSpawn), projectileLifetime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        CancelInvoke();
        OnDeSpawn();
    }
}
