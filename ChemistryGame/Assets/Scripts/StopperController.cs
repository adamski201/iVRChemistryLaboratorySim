using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;


public class StopperController : MonoBehaviour
{
    [SerializeField] private UnityEvent correctTrigger;
    [SerializeField] private UnityEvent incorrectTrigger;
    [SerializeField] private XRSocketInteractor condenserSocket;
    private bool triggered = false;

    void Update()
    {
        HandleTriggers();
    }

    private void HandleTriggers()
    {
        if (!triggered && condenserSocket.hasSelection)
        {
            incorrectTrigger.Invoke();
            triggered = true;
        } 
        else if (triggered && !condenserSocket.hasSelection)
        {
            correctTrigger.Invoke();
            triggered = false;
        }


    }
}
