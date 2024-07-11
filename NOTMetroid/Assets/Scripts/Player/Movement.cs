using UnityEngine;

public class Movement : MonoBehaviour
{

    private Rigidbody2D _rb;
    
    private float _movement;

    private bool _jumping;
    private bool _grounded = true;

    [SerializeField] private float groundDetectDistance;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private StatsSo _stats;

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
    }

    private void Move()
    {
        var movement = new Vector2(Mathf.Lerp(_rb.velocity.x, _movement * _stats.Speed,
            _stats.Friction * Time.fixedDeltaTime), _rb.velocity.y);
        
        _rb.velocity = movement;
    }

    public void MoveEntity(Vector2 movementV)
    {
        _movement = movementV.x;
    }

    public void TriggerJump()
    {
        if (!_grounded) return;
        _jumping = true;
    }

    public void CancelJump()
    {
        if (!_jumping) return;
        
        _rb.velocity = new Vector2(_rb.velocity.x, 0f);
        _jumping = false;
    }

    public void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, _stats.JumpSpeed * Time.fixedDeltaTime);
    }

    private void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundDetectDistance, groundLayer);
        _grounded = hit;
    }

    private void CheckApex()
    {
        var height = Mathf.Approximately(_movement, 0)
            ? _stats.StillJumpHeight
            : _stats.MovingJumpHeight;   
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, height + 0.5f, groundLayer);
         
        Debug.Log(height);
        
        if (Vector2.Distance(transform.position, hit.point) > height)
        {
            _jumping = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, groundDetectDistance);
    }
}
