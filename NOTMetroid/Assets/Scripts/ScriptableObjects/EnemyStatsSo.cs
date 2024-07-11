using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Ayden/EnemyStats")]
public class EnemyStatsSo : ScriptableObject
{
    [field: Header("Gameplay Stats")]
    [field: SerializeField] public float Health { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public float StillJumpHeight { get; private set; }
    [field: SerializeField] public float MovingJumpHeight { get; private set; }
    [field: SerializeField] public float JumpSpeed { get; private set; }
    
    [field: Space(20f), Header("Physics Properties")]
    [field: SerializeField] public float Friction { get; private set; }
}
