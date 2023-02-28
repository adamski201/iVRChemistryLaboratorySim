using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class ClampController : MonoBehaviour
{ 
    // The ClampController class handles functions related to the clamp GameObject 
    
    // Initialize sockets on the clamp stand
    [SerializeField] private XRSocketInteractor correctSocket;
    [SerializeField] private XRSocketInteractor incorrectSocket;

    // Initialize Unity Events, which communicate when a mistake has been made and when it has been corrected
    [SerializeField] private UnityEvent correctTrigger;
    [SerializeField] private UnityEvent incorrectTrigger;

    //Initialize socket on flask
    [SerializeField] private XRSocketInteractor flaskSocket;

    // Ensures that the Unity Events are triggered only once
    private bool triggered = false;

    // Update is called once per frame
    private void Update()
    {
        HandleTriggers();
    }

    // Returns a bool value which indicates that the clamp is in its correct state for reflux success
    public bool IsReady()
    {
        return correctSocket.hasSelection;
    }

    // Monitors the state of the clamp
    private void HandleTriggers()
    {
        // Triggers error event when the clamp is clamped to the condenser
        if (flaskSocket.hasSelection && incorrectSocket.hasSelection && !triggered)
        {
            incorrectTrigger.Invoke();
            triggered = true;
        }

        // Triggers correction event when the clamp is then clamped to the flask
        else if (flaskSocket.hasSelection && correctSocket.hasSelection && triggered)
        {
            correctTrigger.Invoke();
            triggered = false;
        }
    }

}
