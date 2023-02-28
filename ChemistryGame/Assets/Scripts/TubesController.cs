using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class TubesController : MonoBehaviour
{
    // Handles behaviour of the water tubes.

    [SerializeField] private XRSocketInteractor outletSocket;
    [SerializeField] private XRSocketInteractor inletSocket;
    [SerializeField] private UnityEvent correctTrigger;
    [SerializeField] private UnityEvent incorrectTrigger;
    [SerializeField] private LiquidContainer condenser;
    private bool triggered = false;

    private void Update()
    {
        HandleTriggers();
    }

    // Monitors state of tubes for event triggers.
    private void HandleTriggers()
    {
        // If wires haven't been attached but the water is flowing through, triggers error event
        if (!triggered && (!outletSocket.hasSelection || !inletSocket.hasSelection) && condenser.IsFull())
        {
            incorrectTrigger.Invoke();
            triggered = true;
        } 

        // Triggers correction event when the wires are added
        else if (triggered && outletSocket.hasSelection && inletSocket.hasSelection)
        {
            correctTrigger.Invoke();
            triggered = false;
        }
    }
}
