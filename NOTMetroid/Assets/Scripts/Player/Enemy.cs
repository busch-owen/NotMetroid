using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    protected NavMeshAgent _agent;
    protected bool targetInRange;
    protected bool inLungeRange = true;
   [SerializeField] protected float detectionRange;
[SerializeField] protected float speed;   
    [SerializeField] EnemyStatsSo _enemyStats;
    [SerializeField] protected Transform target;
    [SerializeField] protected float lungeRange;
    protected Movement _movement;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.speed = _enemyStats.Speed;
        target = FindObjectOfType<PlayerMovement>().transform;
        _movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
        Vector3 localDir = Quaternion.Inverse(transform.rotation) * (target.transform.position - transform.position);
        
        bool isForward = localDir.z > 0;
        bool isUp = localDir.y > 0;
        bool isRight = localDir.x > 0;
        bool isLeft = localDir.x < 0;
        
        float distance = Vector3.Distance(target.position, this.transform.position);

        float lungeDistance = Vector3.Distance(target.position, this.transform.position );
        
        
        if (distance <= detectionRange)// detection range check, if within set radius between player and enemy then target is set.
        {
            targetInRange = true;
        }
        else
        {
            targetInRange = false;
        }

        if (target != null && targetInRange && _agent.isActiveAndEnabled)
        {
            _agent.SetDestination(target.position);
        }

        if (lungeDistance <= lungeRange && inLungeRange)
        {
            if (isRight)
            {
                _movement.LungeRight();
            }

            if (isLeft)
            {
                _movement.LungeLeft();
            }
            //_movement.TriggerJump();
            
            inLungeRange = false;
            Debug.Log("lunge = false");
        }

        if (lungeDistance >= lungeRange && !inLungeRange)
        {
            inLungeRange = true;
            Debug.Log("lunge = true");
        }
        
        //try adding a time check if grounded and x amount of time passed set inLunge range to true

    }
}
