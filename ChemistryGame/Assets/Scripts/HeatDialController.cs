using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;
using Assets.Scripts;

public class HeatDialController : MonoBehaviour
{
    // The HeatDialController class handles functions related to the dial on the hotplate

    // Initialize Unity Events, which communicate when a mistake has been made and when it has been corrected
    [SerializeField] private UnityEvent correctTrigger;
    [SerializeField] private UnityEvent incorrectTrigger;
    public WhiteboardMessageController whiteboard;

    // Ensures that the Unity Events are triggered only once
    private bool eventHasBeenTriggered = false;

    private DialInteractable dial;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize script
        dial = GetComponent<DialInteractable>();
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleTriggers();
    }

    // Monitors state of dial for events
    private void HandleTriggers()
    {
        // If the heat is too high, triggers an error event.
        if (!eventHasBeenTriggered && dial.Value >= 0.5)
        {
            whiteboard.RaiseMessage(WhiteboardMessage.HEAT_ERROR);
            eventHasBeenTriggered = true;
        } 

        // Triggers correction event when heat is lowered
        else if (eventHasBeenTriggered && dial.Value < 0.5)
        {
            whiteboard.RemoveMessage(WhiteboardMessage.HEAT_ERROR);
            eventHasBeenTriggered = false;
        }
    }

    public bool IsTooHot()
    {
        return dial.Angle >= 0.5;
    }

    public bool IsReady()
    {
        return dial.Angle >= 0.2 && dial.Angle < 0.5;
    }
}
