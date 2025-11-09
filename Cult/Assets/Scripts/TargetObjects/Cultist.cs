using UnityEngine;

public class Cultist : ShootAble
{
    public Animator animator;
    public AudioClip[] deathClips;
    public override void OnShoot()
    {
        Debug.Log("Shot");
        animator.SetBool("Shot", true);
    }

    public void KillCultist()
    {
        SoundManager.instance.PlayRandomSoundClip(deathClips, this.transform, 1f);
        this.gameObject.SetActive(false);
    }
}
