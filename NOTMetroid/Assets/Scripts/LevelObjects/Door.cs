using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject _doorVisual;
    [SerializeField] private Collider2D _doorSolid;
    private bool _isOpen = false;

    private void Awake()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ToggleDoor();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ToggleDoor();
        }
    }

    private void ToggleDoor()
    {
        _isOpen = !_isOpen;
        _doorVisual.SetActive(!_isOpen);
        _doorSolid.gameObject.SetActive(!_isOpen);
    }
}
