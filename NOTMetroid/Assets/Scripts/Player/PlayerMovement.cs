using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    
    private Rigidbody2D _rb;
    
    private float _playerMovement;

    private bool _jumping;
    private bool _movingJump;
    private bool _canWallJump;
    private bool _canDash = true;
    public bool Grounded { get; private set; }

    [SerializeField] private float groundDetectDistance;
    [SerializeField] private Vector2 groundDetectSize;
    [SerializeField] private float wallJumpDetectDistance;
    [SerializeField] private CharacterStatsSO characterStats;

    [SerializeField] private LayerMask ignorePlayer;
    private LayerMask _defaultLayer;
    private LayerMask _invulnLayer;
    public float currentHealth;
    [SerializeField]protected string currentScene;
    private Projectile _projectile;

    #endregion
    
    #region Unity Functions

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _defaultLayer = LayerMask.NameToLayer("Player");
        _invulnLayer = LayerMask.NameToLayer("Invulnerable");
        currentHealth = characterStats.Health;
    }

    private void Update()
    {
        GroundCheck();
    }

    private void FixedUpdate()
    {
        Move();
        
        CheckApex();
        RoofCheck();

        CheckWallJump();
        
        if (_jumping)
        {
            Jump();
        }

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(currentScene);
        }
    }

    #endregion
    
    #region Player Movement

    private void Move()
    {
        var playerMovement = new Vector2(Mathf.Lerp(_rb.velocity.x, _playerMovement * characterStats.Speed,
            characterStats.Friction * Time.fixedDeltaTime), _rb.velocity.y);
        
        _rb.velocity = playerMovement;
    }

    public void MovePlayer(Vector2 movement)
    {
        _playerMovement = movement.x;
        _movingJump = _playerMovement != 0;
    }
    #endregion

    #region Dash Logic

    public void Dash()
    {
        if (!_canDash) return;
        GoInvulnerable();
        _rb.velocity = Vector2.zero;
        _rb.velocity = new Vector2(characterStats.DashSpeed * _playerMovement * Time.fixedDeltaTime, _rb.velocity.y);
        _canDash = false;
        Invoke(nameof(AllowDash), characterStats.DashCooldown);
    }

    private void AllowDash()
    {
        _canDash = true;
    }

    private void GoInvulnerable()
    {

        gameObject.layer = _invulnLayer;
        Invoke(nameof(RemoveInvulnerable), characterStats.InvulnTime);
    }
    
    private void RemoveInvulnerable()
    {
        gameObject.layer = _defaultLayer;
    }

    #endregion
    
    #region Jump Logic

    public void TriggerJump()
    {
        if (_canWallJump)
        {
            WallJump();
            _jumping = true;
            return;
        }
        if (!Grounded) return;
        Debug.Log("Jumping");
        _jumping = true;
    }

    private void CheckWallJump()
    {
        var hit = Physics2D.CircleCast(transform.position, groundDetectDistance, Vector2.zero, Mathf.Infinity, ignorePlayer);
        if (!Grounded && _movingJump)
        {
            _canWallJump = hit;
        }
        else
        {
            _canWallJump = false;
        }
    }

    public void CancelJump()
    {
        if (!_jumping) return;
        
        _rb.velocity = new Vector2(_rb.velocity.x, 0f);
        _jumping = false;
    }

    private void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, characterStats.JumpSpeed * Time.fixedDeltaTime);
    }

    private void WallJump()
    {
        _rb.velocity = Vector2.zero;
        _rb.velocity = new Vector2(characterStats.WallLaunchSpeed * -_playerMovement * Time.fixedDeltaTime, characterStats.WallJumpSpeed * Time.fixedDeltaTime);
    }

    private void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.BoxCast(new Vector2(transform.position.x, 
            transform.position.y - groundDetectDistance), 
            groundDetectSize, 0f, Vector2.zero, Mathf.Infinity, ignorePlayer);
        
        Grounded = hit != false;
    }

    private void RoofCheck()
    {
        RaycastHit2D hit = Physics2D.BoxCast(new Vector2(transform.position.x, 
                transform.position.y + groundDetectDistance), 
            groundDetectSize, 0f, Vector2.zero, Mathf.Infinity, ignorePlayer);
        if (hit)
        {
            CancelJump();
        }
    }

    private void CheckApex()
    {
        var height = Mathf.Approximately(_playerMovement, 0)
            ? characterStats.StillJumpHeight
            : characterStats.MovingJumpHeight;
        
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1.1f), Vector2.down, height - 0.5f, ignorePlayer);
        
        if (Vector2.Distance(transform.position, hit.point) > height)
        {
            _jumping = false;
        }
    }
    
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y - groundDetectDistance), groundDetectSize);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y + groundDetectDistance), groundDetectSize);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, wallJumpDetectDistance);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyProjectile"))
        {
            _projectile = other.GetComponent<Projectile>();
            currentHealth -=_projectile._damage;
        }
    }
}
