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
