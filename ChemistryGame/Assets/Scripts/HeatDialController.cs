using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class HeatDialController : MonoBehaviour
{
    private DialInteractable dial;
    [SerializeField] private UnityEvent correctTrigger;
    [SerializeField] private UnityEvent incorrectTrigger;
    private bool triggered = false;

    // Start is called before the first frame update
    void Start()
    {
        dial = GetComponent<DialInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleTriggers();
    }

    private void HandleTriggers()
    {
        if (!triggered && dial.Value < 0.2)
        {
            incorrectTrigger.Invoke();
            triggered = true;
        } 
        else if (triggered && dial.Value >= 0.2)
        {
            correctTrigger.Invoke();
            triggered = false;
        }
    }
}
