using UnityEngine;

public class Movement : MonoBehaviour
{

    private Rigidbody2D _rb;
    
    [SerializeField] private float _movement;

    private bool _jumping;
    public bool _grounded = true;

    [SerializeField] private float groundDetectDistance;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private EnemyStatsSo _stats;
    [SerializeField] private float _lungeSpeed;
    [SerializeField] private float _moveSpeed;

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

    #region Lunge
    
    public void LungeLeft()
    {
        _rb.AddForce(-transform.right * _lungeSpeed, ForceMode2D.Impulse);
        //Debug.Log("Llunge");
    }

    public void LungeRight()
    {
        _rb.AddForce(transform.right * _lungeSpeed, ForceMode2D.Impulse);
        //Debug.Log("Rlunge");
    }

    public void MoveRight()
    {
        _movement = 0.75f;
        _rb.velocity = new Vector2(Mathf.Lerp(_rb.velocity.x, _movement * _moveSpeed, _stats.Friction * Time.fixedDeltaTime), _rb.velocity.y);
        Debug.Log(_rb.velocity);
    }

    public void MoveLeft()
    {
        _movement = 5.0f;
        _rb.velocity = new Vector2(Mathf.Lerp(-_rb.velocity.x, -_movement * _moveSpeed, _stats.Friction * Time.fixedDeltaTime),_rb.velocity.y);
        Debug.Log(_rb.velocity);
    }
    

    #endregion

    #region Move
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
    
    #endregion

    #region Jump
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
        //Debug.Log(_rb.velocity);
    }
    
    #endregion

    #region Ground & Apex

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
        
        if (Vector2.Distance(transform.position, hit.point) > height)
        {
            _jumping = false;
        }
    }

    #endregion
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, groundDetectDistance);
    }
}
