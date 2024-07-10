using UnityEngine;

[CreateAssetMenu(fileName = "Character Stats", menuName = "AGOS/Stats", order = 1)]
public class CharacterStatsSO : ScriptableObject
{
    [field: Header("Gameplay Stats")]
    [field: SerializeField] public float Health { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public float JumpHeight { get; private set; }
    [field: SerializeField] public float JumpSpeed { get; private set; }
    
    [field: Space(20f), Header("Physics Properties")]
    [field: SerializeField] public float Friction { get; private set; }
    
}