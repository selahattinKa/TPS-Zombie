using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AILocomotion : MonoBehaviour
{
    
    [HideInInspector]
    public NavMeshAgent agent;
    private Animator _animator;

    private ZombieHealth _zombieHealth;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _zombieHealth = GetComponent<ZombieHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (agent.hasPath)
        {
            _animator.SetFloat("Speed", agent.velocity.magnitude);
        }
        else
        {
            _animator.SetFloat("Speed", 0.0f);
        }
        
    }
}
