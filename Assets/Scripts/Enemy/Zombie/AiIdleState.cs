using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiIdleState : AiState
{ 
    public AiStateId GetId()
    {
        return AiStateId.Idle;
    }

    public void Enter(AiAgent agent)
    {
    }

    public void Update(AiAgent agent)
    {
        Vector3 playerDirection = agent.playerTransform.position - agent.transform.position;
        if (playerDirection.magnitude > agent.config.maxSightDistance)
        {
            return;
        }

        Vector3 agentDirection = agent.transform.forward;
        playerDirection.Normalize();
        float dotProdect = Vector3.Dot(playerDirection, agentDirection);
        if (dotProdect > 0.0f)
        {
            agent.StateMachine.ChangeState(AiStateId.ChasePlayer);
        }
    }

    public void Exit(AiAgent agent)
    {
    }
}
