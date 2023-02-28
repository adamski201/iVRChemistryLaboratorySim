using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class FlaskController : MonoBehaviour
{
    // Handles flask behaviour

    // Initializes the hotplate mantle socket
    [SerializeField] private XRSocketInteractor hotplateSocket;

    // Initializes the flask socket (condenser attachment)
    [SerializeField] private XRSocketInteractor flaskSocket;

    // Initialize Unity Events, which communicate when a mistake has been made and when it has been corrected
    [SerializeField] private UnityEvent noGranulesTrigger;
    [SerializeField] private UnityEvent hasGranulesTrigger;

    // Ensures that the Unity Events are triggered only once
    private bool triggered = false;

    private LiquidContainer flask;

    // Start is called before the first frame update
    private void Start()
    {
        // Initialize script
        flask = GetComponent<LiquidContainer>();
    }

    // Update is called once per frame
    private void Update()
    {
        HandleTriggers();
    }

    // Returns a bool value which indicates that the flask is in its correct state for reflux success
    public bool IsReady()
    {
        return flask.IsFull() && hotplateSocket.hasSelection &&
               flaskSocket.hasSelection && flask.containsAntiBumpGranules;
    }

    // Monitors state of flask for event triggers
    private void HandleTriggers()
    {
        // If the condenser has been attached when no anti-bumping granules are contained in the flask, triggers error event
        if (!triggered && !flask.containsAntiBumpGranules && flaskSocket.hasSelection)
        {
            noGranulesTrigger.Invoke();
            triggered = true;
        }

        // Triggers correction event when anti-bump granules are then added
        else if (triggered && flask.containsAntiBumpGranules)
        {
            hasGranulesTrigger.Invoke();
            triggered = false;
        }
    }
}
