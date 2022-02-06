using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    [HideInInspector]
    public LiquidRecipient recipient;

    public List<Ingredient> ingredients;

    private RecipeGenerator recipeGen;

    private void Start()
    {
        recipient = GetComponent<LiquidRecipient>();
        recipient.UpdateAmount();
        //sale mais osef
        recipeGen = GameObject.FindObjectOfType<RecipeGenerator>();
    }

    public void EmptyCauldron()
    {
        recipient.fillAmount = 0.0f;
        recipient.UpdateAmount();
        ingredients.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Bottle":
                LiquidRecipient l = other.GetComponent<LiquidRecipient>();
                if (recipient.fillAmount >= 0.9f && l.fillAmount <= 0.05f)
                {
                    GameObject pot = recipeGen.GetFinalPotion(ingredients);
                    if (pot != null)
                    {
                        l.fillAmount = 1.0f;
                        l.UpdateAmount();
                        l.ingredients = ingredients;
                        l.UpdateColor();
                        print("potion cree, ca fait des bulles");
                    }
                }
                break;
            case "Ingredient":
                IngredientComponent ing = other.GetComponent<IngredientComponent>();
                if (recipient.fillAmount >= 0.9f)
                {
                    ingredients.Add(ing.ingredient);
                    recipient.ingredients = ingredients;
                    recipient.UpdateColor();
                    Destroy(other.gameObject);
                }
                break;
        }
    }

}
