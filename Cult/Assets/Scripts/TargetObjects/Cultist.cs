using UnityEngine;

public class Cultist : ShootAble
{
    public Animator animator;
    public override void OnShoot()
    {
        Debug.Log("Shot");
        animator.SetBool("Shot", true);
    }

    public void KillCultist()
    {
        this.gameObject.SetActive(false);
    }
}
