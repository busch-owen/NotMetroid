using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : Weapon
{
    private Vector2 _aimVector = new Vector2(1, 0);

    private PlayerMovement _player;

    private PlayerAnimationController _animController;

    protected override void Awake()
    {
        base.Awake();
        _player = GetComponent<PlayerMovement>();
        _animController = GetComponent<PlayerAnimationController>();
    }
    
    public void CheckAimAngle(Vector2 vector)
    {
        if (vector == Vector2.zero) return;
        _aimVector = vector;
    }

    public void HandleShoot()
    {
        if (_player.Grounded && _aimVector == new Vector2(0, -1)) return;
        Shoot(_aimVector);
        _animController.CheckRunningShot(_aimVector);
    }

    public void UpdateBeam(WeaponStats newWeapon)
    {
        this.weaponStats = newWeapon;
    }
}
