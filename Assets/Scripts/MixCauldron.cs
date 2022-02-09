using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixCauldron : MonoBehaviour
{
    // Start is called before the first frame update

    private XRJoystick joystick;    

    public int lastX = 0;
    public int lastY =0;

    public Cauldron cauld;

    private const int MAX = 100; 

    [Header("juste pour voir")]
    public float cpt = 0.0f;
    void Start(){
        joystick = GetComponent<XRJoystick>();
        joystick.OnXValueChange.AddListener(OnChangeValueX);
        joystick.OnXValueChange.AddListener(OnChangeValueY);
    }

    public void OnChangeValueX(float v){
        cpt += Mathf.Abs(lastX - v);
        TestAndSend();
    }
    public void OnChangeValueY(float v){
        cpt+= Mathf.Abs(lastY - v);
        TestAndSend();
    }

    private void TestAndSend(){
        if (cpt > MAX){
            cpt = 0;
            cauld.MixPotion();
        }
    }

}
