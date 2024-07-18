using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _playerSprite;
    
    private static readonly int X = Animator.StringToHash("X");
    private static readonly int Y = Animator.StringToHash("Y");
    private static readonly int Running = Animator.StringToHash("Running");
    private static readonly int Shoot = Animator.StringToHash("Shoot");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Ground = Animator.StringToHash("Ground");
    private static readonly int Dash = Animator.StringToHash("Dash");

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void CheckRun(Vector2 input)
    {
        if (input.x == 0)
        {
            _animator.SetBool(Running, false);
            return;
        }
        
        _playerSprite.transform.localScale = new Vector3(input.x, transform.localScale.y, transform.localScale.z);
        _animator.SetBool(Running, true);
    }

    public void CheckRunningShot(Vector2 input)
    {
        _animator.SetFloat(X, Mathf.Abs(input.x));
        _animator.SetFloat(Y, input.y);
        _animator.SetTrigger(Shoot);
    }

    public void CheckGround(bool grounded)
    {
        if (!grounded)
        {
            _animator.SetTrigger(Jump);
            return;
        }
        _animator.SetTrigger(Ground);
    }

    public void CheckDash()
    {
        _animator.SetTrigger(Dash);
    }
}
