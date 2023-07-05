using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    public AiStateMachine StateMachine;

    public AiStateId initialState;

    public NavMeshAgent navMeshAgent;

    public AiAgentConfig config;

    public Ragdoll ragdoll;

    public UIHealthBar ui;
    public Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        navMeshAgent = GetComponent<NavMeshAgent>();
        ragdoll = GetComponent<Ragdoll>();
        ui = GetComponentInChildren<UIHealthBar>();
        StateMachine = new AiStateMachine(this);
        StateMachine.RegisterState(new AiChasePlayerState());
        StateMachine.RegisterState(new AiDeathState());
        StateMachine.RegisterState(new AiIdleState());
        
        StateMachine.ChangeState(initialState);
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine.Update();
    }
}
