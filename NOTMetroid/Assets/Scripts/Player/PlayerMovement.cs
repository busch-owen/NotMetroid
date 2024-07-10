using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D _rb;
    
    private float _playerMovement;

    private bool _jumping;
    private bool _grounded = true;

    [SerializeField] private float groundDetectDistance;

    [SerializeField] private Vector2 groundDetectPos;

    [SerializeField] private LayerMask groundLayer;

    
    [SerializeField] private CharacterStatsSO characterStats;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
        
        CheckApex();
        GroundCheck();

        if (_jumping)
        {
            Jump();
        }
        else
        {
            _rb.velocity = new Vector2(_rb.velocity.x, -characterStats.JumpSpeed * 0.5f * Time.fixedDeltaTime);
        }
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

    public void TriggerJump()
    {
        if (!_grounded) return;
        _jumping = true;
    }

    public void CancelJump()
    {
        _jumping = false;
    }

    private void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, characterStats.JumpSpeed * Time.fixedDeltaTime);
    }

    private void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundDetectDistance, groundLayer);
        _grounded = hit;
    }

    private void CheckApex()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, groundLayer);
        if (Vector2.Distance(transform.position, hit.point) > characterStats.JumpHeight)
        {
            _jumping = false;
            CancelJump();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, groundDetectDistance);
    }
}
