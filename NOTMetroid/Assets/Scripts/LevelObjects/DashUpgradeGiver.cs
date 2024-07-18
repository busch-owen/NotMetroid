using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashUpgradeGiver : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    private PlayerInputHandler _input;
    private void Awake()
    {
        _input = _player.GetComponent<PlayerInputHandler>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _input.EnableDashInput();
            Destroy(this.gameObject);
        }
    }
}
