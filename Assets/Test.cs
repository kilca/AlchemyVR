using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public int cmpt = 0;
    public TextMesh testText;

    public void ChangeText()
    {
        testText.text = "Truc :" + cmpt;
        cmpt++;
    }
    public void ChangeTextOnTrigger()
    {
        testText.text = "Test On Trigger :" + cmpt;
        cmpt++;
    }

}
