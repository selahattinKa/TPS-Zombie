using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public float maxHealth;
    [HideInInspector]
    public float currentHealth;
    public bool isDied = false;
    private UIHealthBar _healthBar;
    private AiAgent _agent;
    private AILocomotion zombie;

    void Start()
    {
        currentHealth = maxHealth;
        _agent = GetComponent<AiAgent>();
        _healthBar = GetComponentInChildren<UIHealthBar>();
        zombie = GetComponent<AILocomotion>();
        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidBody in rigidBodies)
        {
            HitBox hitBox = rigidBody.gameObject.AddComponent<HitBox>();
            hitBox.zombieHealth = this;
        }
    }


    public void TakeDamage(float amount, Vector3 direction)
    {
        currentHealth -= amount;
        _healthBar.SetHealthBarPercentage(currentHealth / maxHealth);
        if (currentHealth <= 0.0f)
        {
            Die(direction);
        }

    }

    void Die(Vector3 direction)
    {
        AiDeathState deathState = _agent.StateMachine.GetState(AiStateId.Death) as AiDeathState;
        deathState.direction = direction;
        _agent.StateMachine.ChangeState(AiStateId.Death);
        ;
    }

}
