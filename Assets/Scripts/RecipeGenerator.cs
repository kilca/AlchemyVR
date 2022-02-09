using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RecipeGenerator : MonoBehaviour
{

    [SerializeField]
    public List<Recipe> recipesList;
    public List<GameObject> globalIngredientList;
    public List<GameObject> finalPotions;
    System.Random random = new System.Random();

    public int chosenRecipe = 0;

    private void Start()
    {
        recipesList = new List<Recipe>();
        generateRecipes(recipesList);
    }

    private void generateRecipes(List<Recipe> recipesList){
        foreach(GameObject potion in finalPotions){ 

            List<GameObject> IngredientList = globalIngredientList.OrderBy(x => random.Next()).Take(3).ToList();

            Recipe recipe = new Recipe(potion, IngredientList);

            recipesList.Add(recipe);
        }
    }

    public bool IsGoodPotion(List<Ingredient> ingredients){
        var checkList1 = ingredients.Select(i => i.id);
        var checkList2 = recipesList[chosenRecipe].ingredientList.Select(g => g.GetComponent<IngredientComponent>().ingredient.id);
        return Enumerable.SequenceEqual(checkList1.OrderBy(t => t), checkList2.OrderBy(t => t));
    }

    public GameObject GetFinalPotion(List<Ingredient> ingredients)
    {
        foreach (var r in recipesList)
        {
            var checkList1 = ingredients.Select(i => i.id);
            var checkList2 = r.ingredientList.Select(g => g.GetComponent<IngredientComponent>().ingredient.id);

            //on compare si les deux sont egaux
            bool eq = Enumerable.SequenceEqual(checkList1.OrderBy(t => t), checkList2.OrderBy(t => t));
            if (eq)
                return r.Potion;
        }
        return null;
    }

}

//pourrait etre plus propre de faire une operator equals qui compare que avec id
[System.Serializable]
public class Ingredient
{
    [SerializeField]
    public Color color;
    public GameObject gameobject;//pas forcemment utile
    public int id;


}

[System.Serializable]
public class Recipe
{
    public GameObject Potion;
    public List<GameObject> ingredientList;

    public Recipe(GameObject Potion, List<GameObject> ingredientList)
    {
        this.Potion = Potion;
        this.ingredientList = ingredientList;
    }
}
