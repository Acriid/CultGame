using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AiAttacking : MonoBehaviour
{
    private NavMeshAgent agent;
    public AiDetection aiDetection;

    void OnEnable()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (aiDetection.canSeePlayer)
        {
            agent.SetDestination(aiDetection.playerRef.transform.position);
        }
    }
}
