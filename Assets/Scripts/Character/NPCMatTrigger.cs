using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMatTrigger : MonoBehaviour
{

    private RecipeGenerator recipeGen;

    public Transform spawnVBuck;
    public GameObject vbuck;
    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        recipeGen = GameObject.FindObjectOfType<RecipeGenerator>();
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bottle")
        {
            NPCClient client = GameObject.FindGameObjectWithTag("Client").GetComponent<NPCClient>();
            Debug.Log("client : " + client);
            Debug.Log("object client : " + GameObject.FindGameObjectWithTag("Client"));
            if (client == null)
                client = FindObjectOfType<NPCClient>();

            LiquidRecipient lr = other.GetComponent<LiquidRecipient>();
            if (recipeGen.IsGoodPotion(lr.ingredients)){
                client.GetPotion();
                Destroy(lr.gameObject);
                Instantiate(vbuck, spawnVBuck.position, Quaternion.identity);
            }
            else
            {
                audio.Play();
            }
        }
    }
}
