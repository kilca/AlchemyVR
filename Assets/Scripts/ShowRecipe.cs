using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRecipe : MonoBehaviour
{
    public Text potionName;
    private List<Recipe> lstRecipe;
    public List<Text> ingredients;
    public Text txtNumPotion;
    public Image imgPotion;
    public int numRecipe = 0;
    public List<Image> lstImage;


    private int nbRecipe;

    private RecipeGenerator generator;

    // Start is called before the first frame update
    void Start()
    {
        generator = GameObject.FindObjectOfType<RecipeGenerator>();
        if (generator == null) print("generator is null");
        StartCoroutine(WaitSeconds());
    }

    //on peut faire en faisant que le script se lance apres mais flm
    IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(0.5f);
        lstRecipe = generator.recipesList;
        if (lstRecipe == null) print("lstRecipe is null");

        nbRecipe = lstRecipe.Count;
        Refresh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PreviousPage()
    {
    
        if (numRecipe == 0)
            numRecipe = nbRecipe-1;
        else
            numRecipe--;

        Refresh();
    }

    public void NextPage()
    {
        if ( numRecipe == nbRecipe-1)   
            numRecipe = 0;
        else
            numRecipe++;

        Refresh();
    }

    public void Refresh()
    {
        Debug.Log("ICIa : "+ ingredients.Count);
        Debug.Log("ICIb : " + lstRecipe.Count);
        Debug.Log("ICIc : " + lstRecipe[numRecipe].ingredientList.Count);

        potionName.text = lstRecipe[numRecipe].Potion.name;
        txtNumPotion.text = (numRecipe+1).ToString();
        ingredients[0].text = lstRecipe[numRecipe].ingredientList[0].name;
        ingredients[1].text = lstRecipe[numRecipe].ingredientList[1].name;
        ingredients[2].text = lstRecipe[numRecipe].ingredientList[2].name;

        //var text = AssetPreview;

        Debug.Log("ICI2");

        //imgPotion.sprite = lstRecipe[numRecipe].Potion.;
        lstImage[0].sprite = lstRecipe[numRecipe].ingredientList[0].GetComponent<IngredientComponent>().sprite;
        lstImage[1].sprite = lstRecipe[numRecipe].ingredientList[1].GetComponent<IngredientComponent>().sprite;
        lstImage[2].sprite = lstRecipe[numRecipe].ingredientList[2].GetComponent<IngredientComponent>().sprite;
        
    }
}
