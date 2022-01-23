using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidRecipient : MonoBehaviour
{
    [Range(0,1)]
    public float fillAmount;

    private Renderer liquidRenderer;

    public float max = -0.21f;
    public float min = 1.22f;

    // Start is called before the first frame update
    void Start()
    {
        liquidRenderer = transform.GetChild(0).GetComponent<Renderer>();
        UpdateAmount();
    }

    //A changer
    private float ValueToAmount(float f) {

        float d = min - max;
        float lvl = f * d;
        return min - lvl;
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
        if (fillAmount == 0.0f)
        {
            liquidRenderer.gameObject.SetActive(false);
        }
        else
        {
            liquidRenderer.gameObject.SetActive(true);
        }
        m.SetFloat("_FillAmount", ValueToAmount(fillAmount));
    }

}
