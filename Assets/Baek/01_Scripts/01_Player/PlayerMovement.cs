using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _character;
    private Vector3 _playerVelocity;
    private bool _isGround;
    private bool _isWalk;
    private float _gravity = -9.8f;
    [SerializeField] private float _jumpPower = 3;
    [SerializeField] private float _speed = 4;
    private void Awake()
    {
        _character = GetComponent<CharacterController>();
    }
    private void Update()
    {
        _isGround = _character.isGrounded;

        if (_character.velocity.magnitude != 0 && _isWalk == false)
        {
            _isWalk = true;
            StartCoroutine("WalkSoundCourutine");
        }

    }
    public void PlayerMove(Vector2 input)
    {
        Vector3 moveDir = Vector3.zero;
        moveDir.x = input.x;
        moveDir.z = input.y;
        _character.Move(transform.TransformDirection(moveDir) * _speed * Time.deltaTime);
        _playerVelocity.y += _gravity * Time.deltaTime;
        if (_isGround && _playerVelocity.y < 0)
            _playerVelocity.y = -2f;
        _character.Move(_playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (_isGround)
        {
            _playerVelocity.y = Mathf.Sqrt(_jumpPower * -3.0f * _gravity);
        }
    }

    public void RunOn()
    {
        _speed *= 2f;
    }

    public void RunOff()
    {
        _speed *= 0.5f;
    }

    private IEnumerator WalkSoundCourutine()
    {
        while (_isWalk && _isGround)
        {
            PoolManager.SpawnFromPool("WalkSound", transform.position);
            yield return new WaitForSeconds(0.7f);
            _isWalk = false;

        }
        _isWalk = false;

    }
}
