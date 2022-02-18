using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

#if UNITY_EDITOR

[CustomEditor(typeof(Cauldron))]
public class CauldonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Cauldron si = (Cauldron)target;
        DrawDefaultInspector();
        if (GUILayout.Button("Mix Potion"))
        {
            si.MixPotion();
        }

    }

}

#endif
public class Cauldron : MonoBehaviour
{
    [HideInInspector]
    public LiquidRecipient recipient;

    public List<Ingredient> ingredients;

    private RecipeGenerator recipeGen;

    private bool isMixed = false;

    public int nbPotion = 2;

    public ParticleSystem particleObject;

    private AudioSource sourceAudio;

    public AudioClip onMixAudio;
    public AudioClip onIngredientAudio;
    public AudioClip doneAudio;

    private void Start()
    {
        sourceAudio = GetComponent<AudioSource>();
        recipient = GetComponent<LiquidRecipient>();
        recipient.UpdateAmount();
        //sale mais osef
        recipeGen = GameObject.FindObjectOfType<RecipeGenerator>();
        particleObject.Stop();
    }

    IEnumerator effect()
    {
        particleObject.Play();
        yield return new WaitForSeconds(5);
        particleObject.Stop();
    }

    public void EmptyCauldron()
    {
        recipient.fillAmount = 0.0f;
        recipient.UpdateAmount();
        ingredients.Clear();
        recipient.ingredients.Clear();
        recipient.ClearColor();
    }

    public void MixPotion(){
        if (recipient.fillAmount >= 0.1f){
            isMixed = true;
            recipient.UpdateColor();
            sourceAudio.clip = onMixAudio;
            sourceAudio.Play();
            nbPotion = 2;
        }
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
                    if (isMixed)
                    {
                        l.fillAmount = 1.0f;
                        l.UpdateAmount();
                        l.ingredients = ingredients;
                        l.UpdateColor();
                        sourceAudio.clip = doneAudio;
                        sourceAudio.Play();
                        print("potion cree, ca fait des bulles");
                        nbPotion--;
                        if (nbPotion == 0)
                            EmptyCauldron();

                    }
                }
                break;
            case "Ingredient":
                IngredientComponent ing = other.GetComponent<IngredientComponent>();
                if (recipient.fillAmount >= 0.9f)
                {
                    isMixed = false;
                    ingredients.Add(ing.ingredient);
                    recipient.ingredients = ingredients;
                    Destroy(other.gameObject);
                    sourceAudio.clip = onIngredientAudio;
                    sourceAudio.Play();
                    StartCoroutine(effect());
                }
                break;
        }
    }

}
