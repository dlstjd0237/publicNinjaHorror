using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcAnimation : MonoBehaviour
{
    private readonly int Run = Animator.StringToHash("Speed");

    private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _animator.SetFloat(Run, _navMeshAgent.velocity.magnitude);
    }
}
