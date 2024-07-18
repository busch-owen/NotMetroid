using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Homing : Projectile

{
    [SerializeField] private Transform target;
    private Rigidbody2D _rb;
    [SerializeField] private float speed;
    public void FixedUpdate()
    {
        
        float distance = Vector3.Distance(target.position, this.transform.position);
        
        Vector3 localDir = Quaternion.Inverse(transform.rotation) * (target.transform.position - transform.position);
        
        bool isForward = localDir.z > 0;
        bool isUp = localDir.y > 0;
        bool isRight = localDir.x > 0;
        bool isLeft = localDir.x < 0;



        if (target != null)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, target.position, Time.deltaTime * speed);
        }
        
    
    }

    protected override void OnEnable()
    {
      base.OnEnable();
      _rb = GetComponent<Rigidbody2D>();
      target = FindObjectOfType<PlayerMovement>().transform;
    }
}