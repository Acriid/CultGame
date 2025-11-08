using UnityEngine;
using UnityEngine.AI;

public class AiPatrolling : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform[] patrolPoints;
    public Animator animator;
    private int currentPoint = 0;
    private AnimatorClipInfo[] animatorStateInfo;
    public AiDetection aiDetection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        if(aiDetection.canSeePlayer) { return; }
        if(patrolPoints.Length == 0) { return; }
        //animatorStateInfo = animator.GetCurrentAnimatorClipInfo(0);
        
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentPoint++;
            if (currentPoint >= patrolPoints.Length)
            {
                currentPoint = 0;
            }
            agent.SetDestination(patrolPoints[currentPoint].position);
        }
        
    }
    public void ResetAnimation()
    {
        
    }
}
