using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapMover : MonoBehaviour
{
    [SerializeField] private int shiftAmt;
    
    public void ShiftMap(Vector2 direction)
    {
        transform.position = new Vector2(transform.position.x + shiftAmt * direction.x, transform.position.y + shiftAmt * direction.y);
    }
}
