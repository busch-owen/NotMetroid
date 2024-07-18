using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] private Camera _currentCamera;
    [SerializeField] private Camera _nextCamera;
    [SerializeField] private Vector2 minimapMoveDir;

    [SerializeField] private Transform teleportPos;
    
    private MinimapMover _minimapMover;
    
    private bool _switch = false;

    private void Start()
    {
        _minimapMover = FindObjectOfType<MinimapMover>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        other.transform.position = teleportPos.position;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        _currentCamera.gameObject.SetActive(false);
        _nextCamera.gameObject.SetActive(true);
        _minimapMover.ShiftMap(minimapMoveDir);
    }
}
