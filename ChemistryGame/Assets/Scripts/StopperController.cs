using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;
using Assets.Scripts;


public class StopperController : MonoBehaviour
{
    // Handles functions related to the stopper GameObject

    // Initializes stopper socket on the condenser
    [SerializeField] private XRSocketInteractor condenserSocket;

    // Ensures that the Unity Events are triggered only once
    private bool triggered = false;
    public WhiteboardMessageController whiteboard;

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
            whiteboard.RaiseMessage(WhiteboardMessage.STOPPER_ERROR);
            triggered = true;
        }

        // Triggers correction event when stopper is removed
        else if (triggered && !condenserSocket.hasSelection)
        {
            whiteboard.RemoveMessage(WhiteboardMessage.STOPPER_ERROR);
            triggered = false;
        }


    }
}
