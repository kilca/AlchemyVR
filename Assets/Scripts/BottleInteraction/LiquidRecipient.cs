using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidRecipient : MonoBehaviour
{
    [Range(0,1)]
    public float fillAmount;

    private Renderer liquidRenderer;


    // Start is called before the first frame update
    void Start()
    {
        liquidRenderer = transform.GetChild(0).GetComponent<Renderer>();
        UpdateAmount();
    }

    //A changer
    private float ValueToAmount(float f) {
        f *=1.4f;
        return (transform.localScale.x - f);
    }

    public void AddLiquid(float quantity) {
        fillAmount = Mathf.Clamp01(fillAmount + quantity);
        UpdateAmount();
    }

    public void RemoveLiquid(float quantity)
    {
        fillAmount = Mathf.Clamp01(fillAmount - quantity);
        UpdateAmount();
    }

    void UpdateAmount() {
        Material m = liquidRenderer.material;
        m.SetFloat("_FillAmount", ValueToAmount(fillAmount));
    }

}
