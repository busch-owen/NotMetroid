using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject _doorVisual;
    [SerializeField] private Collider2D _doorSolid;
    [SerializeField] private bool _isMissileDoor = false;
    [SerializeField] private Sprite _doorSprite1;
    [SerializeField] private Sprite _doorSprite2;
    public UnityEvent doorCloseEvent;
    private SpriteRenderer _doorVisualSprite;
    private bool _isOpen = false;

    private void Awake()
    {
        _doorVisualSprite = _doorVisual.GetComponent<SpriteRenderer>();
        if (_isMissileDoor)
        {
            _doorVisualSprite.sprite = _doorSprite2;
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
            doorCloseEvent?.Invoke();
        }
    }

    private void ToggleDoor()
    {
        _isOpen = !_isOpen;
        _doorVisual.SetActive(!_isOpen);
        _doorSolid.gameObject.SetActive(!_isOpen);
    }

    public void SwitchDoorType()
    {
        _isMissileDoor = !_isMissileDoor;
        _doorVisualSprite.sprite = _doorSprite2;
    }
}
