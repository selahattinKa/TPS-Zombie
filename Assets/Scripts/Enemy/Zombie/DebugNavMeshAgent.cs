using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class DebugNavMeshAgent : MonoBehaviour
{
    public bool velocity;
    public bool desireVelocity;
    public bool path;
    
    private NavMeshAgent _agent;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void OnDrawGizmos()
    {
        if (velocity)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + _agent.velocity);
        }
        if (desireVelocity)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + _agent.desiredVelocity);
        }

        if (path)
        {
            Gizmos.color = Color.black;
            var agentPath = _agent.path;
            Vector3 prevCorner = transform.position;
            foreach (var corner in agentPath.corners)
            {
                Gizmos.DrawLine(prevCorner, corner);
                Gizmos.DrawSphere(corner,0.1f);
                prevCorner = corner;
            }
        }
        
    }
}
