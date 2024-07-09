using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D _rb;
    
    private float _playerMovement;

    
    [SerializeField] private CharacterStatsSO characterStats;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        var playerMovement = new Vector2(Mathf.Lerp(_rb.velocity.x, _playerMovement * characterStats.Speed,
                    characterStats.Friction * Time.fixedDeltaTime), _rb.velocity.y);
        
        _rb.velocity = playerMovement;
    }

    public void MovePlayer(Vector2 movement)
    {
        _playerMovement = movement.x;
    }
}
