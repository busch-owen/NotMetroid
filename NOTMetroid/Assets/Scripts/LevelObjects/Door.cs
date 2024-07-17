using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject _doorVisual;
    [SerializeField] private Collider2D _doorSolid;
    [SerializeField] private bool _isMissileDoor = false;
    private SpriteRenderer _doorVisualSprite;
    private bool _isOpen = false;

    private void Awake()
    {
        _doorVisualSprite = _doorVisual.GetComponent<SpriteRenderer>();
        if (_isMissileDoor)
        {
            _doorVisualSprite.color = new Color(1f, 0f, 0f, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) //this is horribly inefficient
    {
        if (_isMissileDoor)
        {
            if (other.CompareTag("Projectile"))
                Debug.Log("Wrong beam type for door!");
            else if (other.CompareTag("PowerProjectile") && _isOpen == false)
                ToggleDoor();
        }
        else if (other.CompareTag("PowerProjectile") && _isOpen == false)
            ToggleDoor();
        else if (other.CompareTag("Projectile") && _isOpen == false)
            ToggleDoor();
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _isOpen == true)
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
