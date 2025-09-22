using UnityEngine;

public class Cultist : ShootAble
{
    public override void OnShoot()
    {
        gameObject.SetActive(false);
    }
}
