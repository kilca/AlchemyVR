using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PicoButton : XRBaseInteractable
{


    public UnityEvent OnPress;

    public float time;

    private XRBaseInteractor hoverInteractor = null;

    public bool inHover = false;

    // Start is called before the first frame update
    void Start()
    {
        hoverEntered.AddListener(HoverEnter);
        hoverExited.AddListener(HoverExit);
    }

    void HoverEnter(HoverEnterEventArgs eventArgs)
    {
        inHover = true;
    }

    void HoverExit(HoverExitEventArgs eventArgs)
    {
        inHover = false;
    }


    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);
        foreach (var device in inputDevices)
        {
            bool triggerValue;
            if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton,
                                          out triggerValue)
                && triggerValue)
            {
                if (inHover && time > 2)
                {
                    OnPress.Invoke();
                    time = 0;
                }
            }
        }
    }
}
