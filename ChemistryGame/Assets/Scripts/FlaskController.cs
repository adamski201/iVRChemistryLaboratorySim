using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class FlaskController : MonoBehaviour
{
    private LiquidContainer flask;
    [SerializeField] private XRSocketInteractor hotplateSocket;
    [SerializeField] private XRSocketInteractor flaskSocket;
    [SerializeField] private UnityEvent noGranulesTrigger;
    [SerializeField] private UnityEvent hasGranulesTrigger;
    private bool triggered = false;

    private void Start()
    {
        flask = GetComponent<LiquidContainer>();
    }

    private void Update()
    {
        HandleTriggers();
    }

    public bool IsReady()
    {
        return flask.IsFull() && hotplateSocket.hasSelection &&
               flaskSocket.hasSelection && flask.containsAntiBumpGranules;
    }

    private void HandleTriggers()
    {
        if (!triggered && !flask.containsAntiBumpGranules && flaskSocket.hasSelection)
        {
            noGranulesTrigger.Invoke();
            triggered = true;
        }
        else if (triggered && flask.containsAntiBumpGranules)
        {
            hasGranulesTrigger.Invoke();
            triggered = false;
        }
    }
}
