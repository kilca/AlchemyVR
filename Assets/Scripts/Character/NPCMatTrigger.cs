using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMatTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bottle")
        {
            NPCClient client = GameObject.FindGameObjectWithTag("Client").GetComponent<NPCClient>();
            LiquidRecipient lr = other.GetComponent<LiquidRecipient>();

            // if (lr.ingredients)
            // {
                 // client.GetPotion();

            // }
        }
    }
}
