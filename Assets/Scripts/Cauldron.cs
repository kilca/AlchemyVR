using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    [HideInInspector]
    public LiquidRecipient recipient;

    public List<GameObject> ingredientsInCauldron;

    private void Start()
    {
        recipient = GetComponent<LiquidRecipient>();
    }

    public void EmptyCauldron()
    {
        recipient.fillAmount = 0.0f;
        recipient.UpdateAmount();
        ingredientsInCauldron.Clear();
    }

}
