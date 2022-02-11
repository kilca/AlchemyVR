using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class NPCTrigger : MonoBehaviour
{
    public enum Type {Comptoir, Despawn};
    public Type lieu;
    public NPCManager npcManager; 

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter");
        if (other.tag == "Client")
        {
            Debug.Log("client entre ds trigger");
            NPCClient npClient = other.GetComponent<NPCClient>();
            switch (lieu)
            {
                case Type.Comptoir:
                    npClient.ArriveShop();
                    break;
                case Type.Despawn:
                    npClient.ArriveDespawn();
                    NPCManager.lastHasDespawn = true;
                    break;
            }

        }
    }
}
