using UnityEngine;
using UnityEngine.Events;
public class UpgradeChecker : MonoBehaviour
{
    [field: SerializeField] public UnityEvent BeamUpgradeGot { get; private set; }
    [field: SerializeField] public UnityEvent JumpUpgradeGot { get; private set; }
    [field: SerializeField] public UnityEvent DashUpgradeGot { get; private set; }
}
