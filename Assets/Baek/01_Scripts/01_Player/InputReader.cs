using UnityEngine;
using static PlayerInput;
[CreateAssetMenu(menuName = "ScriptableObject/InputReader")]
public class InputReader : ScriptableObject
{
    private PlayerInput _playerInput;
    public PlayerInput PlayerInput()
    {
        return _playerInput;
    }

    private void OnEnable()
    {
        if (_playerInput == null)
        {
            _playerInput = new PlayerInput();
        }

        _playerInput.Enable();
    }

    public void OnFloor()
    {
        _playerInput._onFloor.Enable();
    }
    public void OffFloor()
    {
        _playerInput._onFloor.Disable();
    }
}
