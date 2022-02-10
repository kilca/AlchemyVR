using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomButton : XRBaseInteractable
{    
    [Serializable] public class ValueChangeEvent : UnityEvent { }

    public bool hasClicked = false;

    public ValueChangeEvent OnClick = new ValueChangeEvent();

    private XRBaseInteractor selectInteractor = null;

    void Update(){
        //time+=time.deltaTime;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        selectEntered.AddListener(StartGrab);
        selectExited.AddListener(EndGrab);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        selectEntered.RemoveListener(StartGrab);
        selectExited.RemoveListener(EndGrab);
    }

    private void StartGrab(SelectEnterEventArgs eventArgs)
    {
        selectInteractor = eventArgs.interactor;
    }

    private void EndGrab(SelectExitEventArgs eventArgs)
    {
        selectInteractor = null;
        hasClicked = false;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (isSelected)
        {
            if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
            {
                if (!hasClicked){
                    OnClick.Invoke();
                    hasClicked = true;
                }
            }
        }
    }
}
