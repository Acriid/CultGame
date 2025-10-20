using UnityEngine;
using UnityEngine.AI;

public class AiPatrolling : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform[] patrolPoints;
    public Animator animator;
    private int currentPoint = 0;
    private AnimatorClipInfo[] animatorStateInfo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        animatorStateInfo = animator.GetCurrentAnimatorClipInfo(0);
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
