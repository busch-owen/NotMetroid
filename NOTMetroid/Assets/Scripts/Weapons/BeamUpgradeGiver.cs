using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamUpgradeGiver : MonoBehaviour
{
    [SerializeField] private WeaponStats _beamUpgrade;
    [SerializeField] private GameObject _player;
    private PlayerWeapon _playerWeapon;

    private void Awake()
    {
        _playerWeapon = _player.GetComponent<PlayerWeapon>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerWeapon.UpdateBeam(_beamUpgrade);
            Destroy(this.gameObject);
        }
    }
}
