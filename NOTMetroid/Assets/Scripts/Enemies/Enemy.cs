using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    protected bool targetInRange;
    protected bool inLungeRange = true;
    [SerializeField] protected float detectionRange;
    [SerializeField] private float _range;
    [SerializeField] protected float speed;   
    [SerializeField] EnemyStatsSo _enemyStats;
    [SerializeField] protected Transform target;
    [SerializeField] protected float lungeRange;
    protected Movement _movement;
    private Rigidbody2D _rb;
    private bool invokeRunning = false;
    protected float currentHealth;
    private Projectile _projectile;
    [SerializeField] private bool canJump;
    [SerializeField] private bool patroling = false;
    [SerializeField] private bool _right = true;
    [SerializeField] private bool _left;
    [SerializeField] private bool canMove;
    private bool canShoot;
    

    private EnemyWeapon _weapon;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform;
        _movement = GetComponent<Movement>();
        _rb = GetComponent<Rigidbody2D>();
        currentHealth = _enemyStats.Health;
        _weapon = GetComponent<EnemyWeapon>();

    }
    
    void Loop()
    {
        inLungeRange = true;
    }
    
    void Right()
    {
        _movement.LungeRight();
        inLungeRange = false;    
    }

    void Left()
    {
        _movement.LungeLeft();
        inLungeRange = false;
    }
    

    private void FixedUpdate()
    {
        #region directionCalculation
        
        Vector3 localDir = Quaternion.Inverse(transform.rotation) * (target.transform.position - transform.position);
        
        bool isForward = localDir.z > 0;
        bool isUp = localDir.y > 0;
        bool isRight = localDir.x > 0;
        bool isLeft = localDir.x < 0;
        #endregion

        #region Distance Calculation
        
        float distance = Vector3.Distance(target.position, this.transform.position);
        
        float gunRange = Vector3.Distance(target.position, this.transform.position);

        float lungeDistance = Vector3.Distance(target.position, this.transform.position );
        
        #endregion

        #region InRangeFollow
        
        if (distance <= detectionRange && canMove)// detection range check, if within set radius between player and enemy then target is set.
        {
            targetInRange = true;
            patroling = false;
        }
        else
        {
            targetInRange = false;
            CancelInvoke("Loop");
            invokeRunning = false;
            

        }

        if (gunRange <= _range)
        {
            canShoot = true;
            
        }
        else
        {
            canShoot = false;
        }
        
        #endregion

        #region LungeCheck

        if (target != null && targetInRange && canJump)

        {
            if (!invokeRunning)
            {
                InvokeRepeating("Loop", 1, 2);
                invokeRunning = true;
            }

            
            if (isRight && inLungeRange)
            {
                if (_movement._grounded)
                {
                    _movement.Jump();
                    Invoke("Right", 1.0f);
                }
            }

            if (isLeft && inLungeRange)
            {
                if(_movement._grounded)
                {
                    _movement.Jump();
                    Invoke("Left", 1.0f);
                }
            }
        }
        #endregion
        
        #region MoveCheck
        if (canJump == false && target != null && targetInRange)
        {
            if (isRight)
            {
                _movement.MoveRight();
                //Debug.Log("Right");
            }

            if (isLeft)
            {
                _movement.MoveLeft();
                //Debug.Log("Left");
            }
        }
        
        
        
        #endregion

        #region Patrol

        


        
        if (patroling)// movement logic
        {
            if (_right && patroling)
            {
                _movement.MoveRight();
            }

            if (_left && patroling)
            {
                _movement.MoveLeft();
            }
        }
        
        #endregion

        if (canShoot && isRight)
        {
            //Debug.Log("shoot");
            _weapon.Shoot(this.transform.right);
            
        }

        if (canShoot && isLeft)
        {
            //Debug.Log("shoot");
            _weapon.Shoot(-this.transform.right);
        }
        
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }

        //try adding a time check if grounded and x amount of time passed set inLunge range to true

    }

    private void OnTriggerEnter2D(Collider2D other)// die when hit by bullet
    {
        if (other.CompareTag("Projectile"))
        {
            _projectile = other.GetComponent<Projectile>();
            Debug.Log("EA");
            currentHealth -=_projectile._damage;
        }

        if (patroling)
        {
            if (other.CompareTag("RightPatrolPoint"))
            {
                Debug.Log("patrolRight");
                _right = false;
                _left = true;
            }

            if (other.CompareTag("LeftPatrolPoint"))// switches patrol point and direction once touched
            {
                Debug.Log("patrolleft");
                _left = false;
                _right = true;
            }
        }

        
        // set velocity to zero once hit ground after lunge.
    }
}
