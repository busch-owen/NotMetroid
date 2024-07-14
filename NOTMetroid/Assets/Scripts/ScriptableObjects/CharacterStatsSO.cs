using UnityEngine;

[CreateAssetMenu(fileName = "Character Stats", menuName = "AGOS/Stats/CharacterStats", order = 1)]
public class CharacterStatsSO : ScriptableObject
{
    [field: Header("Gameplay Stats")]
    [field: SerializeField] public float Health { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public float StillJumpHeight { get; private set; }
    [field: SerializeField] public float MovingJumpHeight { get; private set; }
    [field: SerializeField] public float JumpSpeed { get; private set; }
    [field: SerializeField] public float WallJumpSpeed { get; private set; }
    [field: SerializeField] public float WallLaunchSpeed { get; private set; }
    
    [field: SerializeField] public float DashSpeed { get; private set; }
    [field: SerializeField] public float DashCooldown { get; private set; }
    [field: SerializeField] public float InvulnTime { get; private set; }
    
    
    [field: Space(20f), Header("Physics Properties")]
    [field: SerializeField] public float Friction { get; private set; }
    
}