using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;


public class StopperController : MonoBehaviour
{
    // Handles functions related to the stopper GameObject

    // Initialize Unity Events, which communicate when a mistake has been made and when it has been corrected
    [SerializeField] private UnityEvent correctTrigger;
    [SerializeField] private UnityEvent incorrectTrigger;

    // Initializes stopper socket on the condenser
    [SerializeField] private XRSocketInteractor condenserSocket;

    // Ensures that the Unity Events are triggered only once
    private bool triggered = false;

    // Update is called once per frame
    void Update()
    {
        HandleTriggers();
    }

    // Monitors state of stopper for events
    private void HandleTriggers()
    {
        // Triggers error event when stopper is attached to condenser
        if (!triggered && condenserSocket.hasSelection)
        {
            incorrectTrigger.Invoke();
            triggered = true;
        }

        // Triggers correction event when stopper is removed
        else if (triggered && !condenserSocket.hasSelection)
        {
            correctTrigger.Invoke();
            triggered = false;
        }


    }
}
