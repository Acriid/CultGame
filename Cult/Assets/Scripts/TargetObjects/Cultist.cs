using UnityEngine;
using UnityEngine.AI;

public class Cultist : ShootAble
{
    public Animator animator;
    public AudioClip[] deathClips;
    public NavMeshAgent navMeshAgent;
    public override void OnShoot()
    {
        Debug.Log("Shot");
        animator.SetBool("Shot", true);
        navMeshAgent.isStopped = true;
    }

    public void KillCultist()
    {
        SoundManager.instance.PlayRandomSoundClip(deathClips, this.transform, 1f);
        this.gameObject.SetActive(false);
    }
}
