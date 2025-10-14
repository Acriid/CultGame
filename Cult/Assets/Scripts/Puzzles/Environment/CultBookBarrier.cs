using UnityEngine;
using UnityEngine.Rendering;

public class CultBookBarrier : MonoBehaviour
{
    public ItemSO bookSO;
    public Material originalMaterial;
    public float aplhaValue = 0.75f;
    private MeshCollider meshCollider;
    Color ColorValues;
    void Awake()
    {
        ColorValues = originalMaterial.GetColor("_Base_Color");
        meshCollider = this.GetComponent<MeshCollider>();
    }
    void OnEnable()
    {
        bookSO.OnIsEquipedChange += OnBookEquip;
    }
    void OnDisable()
    {
        bookSO.OnIsEquipedChange -= OnBookEquip;
    }
    void OnDestroy()
    {
        OnDisable();
    }

    private void OnBookEquip(bool newValue)
    {
        if (newValue)
        {
            ColorValues.a = aplhaValue;
            meshCollider.enabled = false;
        }
        else if (!newValue)
        {
            ColorValues.a = 1f;
            meshCollider.enabled = true;
        }

        originalMaterial.SetColor("_Base_Color", ColorValues);
    }



}