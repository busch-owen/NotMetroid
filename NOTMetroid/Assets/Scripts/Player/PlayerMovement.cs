using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D _rb;
    
    private float _playerMovement;

    private bool _jumping;
    private bool _movingJump;
    private bool _canWallJump;
    public bool Grounded { get; private set; } = true;

    [SerializeField] private float groundDetectDistance;
    [SerializeField] private float wallJumpDetectDistance;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
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
        RoofCheck();

        CheckWallJump();
        
        if (_jumping)
        {
            Jump();
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
        _movingJump = _playerMovement != 0;
    }

    public void TriggerJump()
    {
        if (_canWallJump)
        {
            WallJump();
            _jumping = true;
            return;
        }
        if (!Grounded) return;
        _jumping = true;
    }

    private void CheckWallJump()
    {
        var hit = Physics2D.CircleCast(transform.position, groundDetectDistance, Vector2.zero, Mathf.Infinity, wallLayer);
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundDetectDistance, groundLayer);
        Grounded = hit;
    }

    private void RoofCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, groundDetectDistance, groundLayer);
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
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, height + 0.5f, groundLayer);
        
        if (Vector2.Distance(transform.position, hit.point) > height)
        {
            _jumping = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, groundDetectDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, wallJumpDetectDistance);
    }
}
