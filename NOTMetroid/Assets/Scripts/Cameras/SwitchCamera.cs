using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera1;
    [SerializeField] private Camera _camera2;
    [SerializeField] private Vector2 minimapMoveDir;
    
    private MinimapMover _minimapMover;
    
    private bool _switch = false;

    private void Start()
    {
        _minimapMover = FindObjectOfType<MinimapMover>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!_switch)
            {
                _camera1.gameObject.SetActive(false);
                _camera2.gameObject.SetActive(true);
                _minimapMover.ShiftMap(minimapMoveDir);
                _switch = !_switch;
            }
            else
            {
                _camera2.gameObject.SetActive(false);
                _camera1.gameObject.SetActive(true);
                _minimapMover.ShiftMap(-minimapMoveDir);
                _switch = !_switch;
            }
        }

    }
}
