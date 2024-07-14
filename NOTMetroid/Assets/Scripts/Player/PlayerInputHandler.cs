using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private PlayerWeapon _playerWeapon;

    private GameInput _gameInput;

    private void OnEnable()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerWeapon = GetComponent<PlayerWeapon>();

        if (_gameInput != null) return;
        
        _gameInput = new GameInput();
        _gameInput.PlayerMovement.Move.performed += i => _playerMovement.MovePlayer(i.ReadValue<Vector2>());
        _gameInput.PlayerMovement.Move.performed += i => _playerWeapon.CheckAimAngle(i.ReadValue<Vector2>());
        _gameInput.PlayerMovement.Jump.started += i => _playerMovement.TriggerJump();
        _gameInput.PlayerMovement.Jump.canceled += i => _playerMovement.CancelJump();

        _gameInput.PlayerActions.Shoot.started += i => _playerWeapon.HandleShoot();
        _gameInput.PlayerActions.Dash.started += i => _playerMovement.Dash();
        
        _gameInput.Enable();
    }

    private void OnDestroy()
    {
        _gameInput.Disable();
    }
}
