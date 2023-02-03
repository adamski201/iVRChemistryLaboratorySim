using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class ClampController : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor correctSocket;
    [SerializeField] private XRSocketInteractor incorrectSocket;
    [SerializeField] private UnityEvent correctTrigger;
    [SerializeField] private UnityEvent incorrectTrigger;
    [SerializeField] private XRSocketInteractor flaskSocket;
    private bool triggered = false;

    private void Update()
    {
        HandleTriggers();
    }

    public bool IsReady()
    {
        return correctSocket.hasSelection;
    }

    private void HandleTriggers()
    {
        if (flaskSocket.hasSelection && incorrectSocket.hasSelection && !triggered)
        {
            incorrectTrigger.Invoke();
            triggered = true;
        }
        else if (flaskSocket.hasSelection && correctSocket.hasSelection && triggered)
        {
            correctTrigger.Invoke();
            triggered = false;
        }
    }

}
