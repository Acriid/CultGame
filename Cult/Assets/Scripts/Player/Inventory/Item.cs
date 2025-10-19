using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemSO itemSO;
    [SerializeField] private MonoBehaviour script; 
    public void ActivateScript()
    {
        if (script != null)
        {
            script.enabled = true; 
        }

    }
    public void DeActivateScript()
    {
        if (script != null)
        {
            script.enabled = false; 
        }
    }
}
