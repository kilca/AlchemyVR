using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMatTrigger : MonoBehaviour
{

    private RecipeGenerator recipeGen;

    // Start is called before the first frame update
    void Start()
    {
        recipeGen = GameObject.FindObjectOfType<RecipeGenerator>();   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bottle")
        {
            NPCClient client = GameObject.FindGameObjectWithTag("Client").GetComponent<NPCClient>();
            LiquidRecipient lr = other.GetComponent<LiquidRecipient>();
            if (recipeGen.IsGoodPotion(lr.ingredients)){
                Destroy(lr.gameObject);
                client.GetPotion();
            }
        }
    }
}
