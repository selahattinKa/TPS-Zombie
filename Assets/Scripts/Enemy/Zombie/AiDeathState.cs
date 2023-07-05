using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDeathState : AiState
{

    public Vector3 direction;
    public AiStateId GetId()
    {
        return AiStateId.Death;
        
    }

    public void Enter(AiAgent agent)
    {
        agent.ragdoll.ActivateRagdoll();
        direction.y = 1f;
        agent.ragdoll.ApplForce(direction * agent.config.dieForce);
        agent.ui.gameObject.SetActive(false);    
    }

    public void Update(AiAgent agent)
    {
    }

    public void Exit(AiAgent agent)
    {
    }
}
