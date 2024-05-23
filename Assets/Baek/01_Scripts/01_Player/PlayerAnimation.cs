using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private readonly int y = Animator.StringToHash("Vector3.Y");
    private readonly int x = Animator.StringToHash("Vector3.X");


    private Rigidbody _rig;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rig = GetComponent<Rigidbody>();
    }

    public void PlayerMoveAnimation(Vector3 dir)
    {
        _animator.SetFloat(y, dir.y, .1f, Time.deltaTime);
        _animator.SetFloat(x, dir.x, .1f, Time.deltaTime);
    }


}
