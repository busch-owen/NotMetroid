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
    [SerializeField] protected float speed;   
    [SerializeField] EnemyStatsSo _enemyStats;
    [SerializeField] protected Transform target;
    [SerializeField] protected float lungeRange;
    protected Movement _movement;
    private Rigidbody2D _rb;
    private bool invokeRunning = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform;
        _movement = GetComponent<Movement>();
        _rb = GetComponent<Rigidbody2D>();
        
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

        float lungeDistance = Vector3.Distance(target.position, this.transform.position );
        
        #endregion

        #region InRangeFollow
        
        if (distance <= detectionRange)// detection range check, if within set radius between player and enemy then target is set.
        {
            targetInRange = true;
        }
        else
        {
            targetInRange = false;
            CancelInvoke("Loop");
            invokeRunning = false;

        }
        
        #endregion

        #region LungeCheck

        if (target != null && targetInRange)

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
        
        //try adding a time check if grounded and x amount of time passed set inLunge range to true

    }
    
}
