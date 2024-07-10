using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerMovement _playerMovement;

    private GameInput _gameInput;

    private void OnEnable()
    {
        _playerMovement = GetComponent<PlayerMovement>();

        if (_gameInput != null) return;
        
        _gameInput = new GameInput();
        _gameInput.PlayerMovement.Move.performed += i => _playerMovement.MovePlayer(i.ReadValue<Vector2>());
        _gameInput.PlayerMovement.Jump.started += i => _playerMovement.TriggerJump();
        _gameInput.PlayerMovement.Jump.canceled += i => _playerMovement.CancelJump();
        
        _gameInput.Enable();
    }

    private void OnDestroy()
    {
        _gameInput.Disable();
    }
}
