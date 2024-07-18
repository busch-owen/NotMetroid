using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapMover : MonoBehaviour
{
    [SerializeField] private int shiftAmount;
    
    //To use this script you must call this shift map function, which at the moment is only getting called from the SwitchCamera class
    //Input a direction and the map will shift in that direction when you move between rooms so long as there is a trigger between them
    
    public void ShiftMap(Vector2 direction)
    {
        transform.position = new Vector2(transform.position.x + shiftAmount * direction.x, transform.position.y + shiftAmount * direction.y);
    }
}
