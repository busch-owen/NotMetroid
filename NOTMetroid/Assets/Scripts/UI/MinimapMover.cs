using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapMover : MonoBehaviour
{
    [SerializeField] private int shiftAmount;
    
    public void ShiftMap(Vector2 direction)
    {
        transform.position = new Vector2(transform.position.x + shiftAmount * direction.x, transform.position.y + shiftAmount * direction.y);
    }
}
