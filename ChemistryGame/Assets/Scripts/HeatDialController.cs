using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class HeatDialController : MonoBehaviour
{
    // The HeatDialController class handles functions related to the dial on the hotplate

    // Initialize Unity Events, which communicate when a mistake has been made and when it has been corrected
    [SerializeField] private UnityEvent correctTrigger;
    [SerializeField] private UnityEvent incorrectTrigger;

    // Ensures that the Unity Events are triggered only once
    private bool triggered = false;

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
        if (!triggered && dial.Value < 0.2)
        {
            incorrectTrigger.Invoke();
            triggered = true;
        } 

        // Triggers correction event when heat is lowered
        else if (triggered && dial.Value >= 0.2)
        {
            correctTrigger.Invoke();
            triggered = false;
        }
    }
}
