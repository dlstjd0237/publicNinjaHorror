using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private InputReader _input;
    private PlayerInput _playerInput;
    public PlayerInput._onFloorActions OnFloor;
    private PlayerMovement _playerMovement;
    private PlayerLook _playerLook;
    private PlayerAnimation _playerAnimation;
    private Inventory _inventory;

    [SerializeField] private Transform _dropTrm;
    private void Awake()
    {
        Init();

        if (_input == null)
            _input = Resources.Load<InputReader>("Baek/PlayerInputReader");
        if (_playerInput == null)
            _playerInput = _input.PlayerInput();

        OnFloor = _playerInput._onFloor;
        OnFloor.Jump.performed += ctx => _playerMovement.Jump();
        OnFloor.RunOn.performed += ctx => _playerMovement.RunOn();
        OnFloor.RunOff.performed += ctx => _playerMovement.RunOff();
        OnFloor.Quest.performed += ctx => QuestManager.Instance.Questing();
        OnFloor.Drop.performed += ctx => _inventory.DropItem(_dropTrm);

    }


    private void Init()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _playerMovement = GetComponent<PlayerMovement>();
        _playerLook = GetComponent<PlayerLook>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _inventory = GetComponent<Inventory>();
    }

    private void Update()
    {
        _playerMovement.PlayerMove(OnFloor.Move.ReadValue<Vector2>());
        _playerLook.ProcessLook(OnFloor.Look.ReadValue<Vector2>());
        _playerAnimation?.PlayerMoveAnimation(OnFloor.Move.ReadValue<Vector2>().normalized);
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }
    private void OnDisable()
    {
        _playerInput.Disable();
    }
}
