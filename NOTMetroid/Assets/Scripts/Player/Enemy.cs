using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    protected NavMeshAgent _agent;
    protected bool targetInRange;
    protected bool inLungeRange;
   [SerializeField] protected float detectionRange;
   [SerializeField] protected float speed;
    protected StatsSo _enemyStats;
    [SerializeField] protected Transform target;
    [SerializeField] protected float lungeRange;
    protected Movement _movement;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.speed = _enemyStats.Speed;
        target = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        _agent.updateRotation = false;
    }

    private void FixedUpdate()
    {
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
            _agent.speed = speed;
            _movement.Jump();
            inLungeRange = false;
            Debug.Log("lunge = false");
        }

        if (lungeDistance >= lungeRange && !inLungeRange)
        {
            inLungeRange = true;
            Debug.Log("lunge = true");
        }

    }
}
