using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : Weapon
{
    private Vector2 _aimVector = new Vector2(1, 0);

    private PlayerMovement _player;

    protected override void Awake()
    {
        base.Awake();
        _player = GetComponent<PlayerMovement>();
    }
    
    public void CheckAimAngle(Vector2 vector)
    {
        if (vector == Vector2.zero) return;
        _aimVector = vector;
        Debug.Log(_aimVector);
    }

    public void HandleShoot()
    {
        if (_player.Grounded && _aimVector == new Vector2(0, -1)) return;
        Shoot(_aimVector);
    }
}
