using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpUpgradeGiver : MonoBehaviour
{
    [SerializeField] private CharacterStatsSO _playerStats;

    private void Awake()
    {
        _playerStats.JumpSpeed = 500; //probably should be reset in the player controller but i'm short on time here!
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerStats.JumpSpeed = 750;
            Destroy(this.gameObject);
        }
    }
}
