using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class TubesController : MonoBehaviour
{
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

    private void HandleTriggers()
    {
        if (!triggered && (!outletSocket.hasSelection || !inletSocket.hasSelection) && condenser.IsFull())
        {
            incorrectTrigger.Invoke();
            triggered = true;
            
        } else if (triggered && outletSocket.hasSelection && inletSocket.hasSelection)
        {
            correctTrigger.Invoke();
            triggered = false;
        }
    }
}
