using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
    private GameObject enemySprite;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool isFlashing;


    private EnemyWeapon _weapon;




    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform;
        _movement = GetComponent<Movement>();
        _rb = GetComponent<Rigidbody2D>();
        currentHealth = _enemyStats.Health;
        _weapon = GetComponent<EnemyWeapon>();
        enemySprite = GetComponentInChildren<SpriteRenderer>().gameObject;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = spriteRenderer.color;

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


    void FixedUpdate()
    {
        #region directionCalculation

        Vector3 localDir = Quaternion.Inverse(transform.rotation) * (target.transform.position - transform.position);

        /*var Higher = Math.Max(localDir.x, localDir.y);

        var Lower = Math.Min(localDir.x, localDir.y);


        if (Higher == localDir.x)
        {
            bool UpDown = true;
            bool LeftRight = false;
        }

        if (Higher == localDir.y)
        {
            bool LeftRight = true;
            bool UpDown = false;
        }

        if (Higher > 0)
        {
            Higher = 1.0f;
        }

        if (Higher < 0)
        {
            Higher = -1.0f;
        }


        Lower = 0.0f;



        Debug.Log(Mathf.Max(localDir.x, localDir.y));*/







        bool isDown = localDir.y < 0;
        bool isUp = localDir.y > 0;
        bool isRight = localDir.x > 0;
        bool isLeft = localDir.x < 0;

        if (isRight)
        {
            enemySprite.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            enemySprite.transform.localScale = new Vector3(1, 1, 1);
        }

        #endregion

        #region Distance Calculation

        float distance = Vector3.Distance(target.position, this.transform.position);

        float gunRange = Vector3.Distance(target.position, this.transform.position);

        float lungeDistance = Vector3.Distance(target.position, this.transform.position);

        #endregion

        #region InRangeFollow

        if (distance <= detectionRange &&
            canMove) // detection range check, if within set radius between player and enemy then target is set.
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
                if (_movement._grounded)
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





        if (patroling) // movement logic
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

        /*if (canShoot && isUp)
        {
            //Debug.Log("up");
            _weapon.Shoot(this.transform.up);

        }

        if (canShoot && isDown)
        {
            //Debug.Log("down");
            _weapon.Shoot(-this.transform.up);
        }*/



        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }

        //try adding a time check if grounded and x amount of time passed set inLunge range to true

    }
    
    void CancleCoroutine()
    {
        StopAllCoroutines();
    }

    void OnTriggerEnter2D(Collider2D other) // die when hit by bullet
    {
        if (other.CompareTag("Projectile"))
        {
            StartFlash();
            _projectile = other.GetComponent<Projectile>();
            //Debug.Log("EA");
            currentHealth -= _projectile._damage;
            Invoke("CancleCoroutine",1.0f);

        }

        if (other.CompareTag("PowerProjectile"))
        {
            StartFlash();
            _projectile = other.GetComponent<Projectile>();
            //Debug.Log("EA");
            currentHealth -= _projectile._damage;
            Invoke("CancleCoroutine",1.0f);
        }

        if (patroling)
        {
            if (other.CompareTag("RightPatrolPoint"))
            {
                //Debug.Log("patrolRight");
                _right = false;
                _left = true;
            }

            if (other.CompareTag("LeftPatrolPoint")) // switches patrol point and direction once touched
            {
                // Debug.Log("patrolleft");
                _left = false;
                _right = true;
            }
        }

        void StartFlash()
        {
                StartCoroutine(FlashCoroutine());
        }

        IEnumerator FlashCoroutine()
        {
            Debug.Log("Flash");

            Color flashColor = Color.red;

            for (int i = 0; i < 1; i++) // Flash 5 times (adjust as needed)
            {
                spriteRenderer.color = flashColor;
                yield return new WaitForSeconds(0.2f); // Flash duration
                spriteRenderer.color = originalColor;
                yield return new WaitForSeconds(0.2f); // Delay between flashes
            }
        }
    }
}
