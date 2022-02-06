using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LiquidRecipient : MonoBehaviour
{
    [Range(0,1)]
    public float fillAmount;

    private Renderer liquidRenderer;

    public float max = -0.21f;
    public float min = 1.22f;

    public List<Ingredient> ingredients;

    // Start is called before the first frame update
    void Awake()
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

    public static Color CombineColors(params Color[] aColors)
    {
        Color result = new Color(0, 0, 0, 0);
        foreach (Color c in aColors)
        {
            result += c;
        }
        result /= aColors.Length;
        return result;
    }

    public void UpdateColor() {
        Color finalColor;
        if (ingredients.Count == 0)
        {
            finalColor = Color.blue;
        }
        else
        {
            finalColor = CombineColors(ingredients.Select(i => i.color).ToArray());
        }
        Material m = liquidRenderer.material;
        m.SetColor("_Tint", finalColor);
        m.SetColor("_TopColor", finalColor);

    }

    public void UpdateAmount() {
        print(transform.GetChild(0));
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
