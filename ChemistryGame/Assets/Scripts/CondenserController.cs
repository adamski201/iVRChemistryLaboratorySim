using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class CondenserController : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor intakeSocket;
    [SerializeField] private XRSocketInteractor outtakeSocket;
    [SerializeField] private Transform intakePipe;
    [SerializeField] private Transform outtakePipe;
    [SerializeField] private Collider intakeCollider;
    [SerializeField] private Collider outtakeCollider;
    [SerializeField] private DialInteractable dial;
    [SerializeField] private Material material;
    [SerializeField] private UnityEvent incorrectTrigger;
    [SerializeField] private UnityEvent correctTrigger;
    private LiquidContainer condenser;
    private bool triggered = false;
    private bool tubesAttached = false;
    private bool tubesCorrectlyAttached = false;
    [HideInInspector] public bool isReady = false;

    private void Start()
    {
        condenser = gameObject.GetComponent<LiquidContainer>();
    }

    void Update()
    {
        CheckTubesAttached();
        HandleCondenserInternal();
        HandleSockets();
    }

    private void HandleSockets()
    {
        if (dial.Value < 0.2)
        {
            intakeCollider.gameObject.SetActive(false);
            outtakeCollider.gameObject.SetActive(false);
        } else
        {
            intakeCollider.gameObject.SetActive(true);
            outtakeCollider.gameObject.SetActive(true);
        }
    }

    private void CheckTubesAttached()
    {
        if (intakeSocket.hasSelection && outtakeSocket.hasSelection)
        {
            tubesAttached = true;
        }
        else
        {
            tubesAttached = false;
        }

        if (tubesAttached && outtakePipe.localPosition.y < intakePipe.localPosition.y)
        {
            tubesCorrectlyAttached = true;
            if (triggered)
            {
                correctTrigger.Invoke();
                triggered = false;
            }
        }
        else if (tubesAttached && outtakePipe.localPosition.y > intakePipe.localPosition.y)
        {
            tubesCorrectlyAttached = false;
            if (!triggered && dial.Value <= 0.2)
            {
                incorrectTrigger.Invoke();
                triggered = true;
            }
        }
    }

    private void HandleCondenserInternal()
    {        
        if (tubesCorrectlyAttached && dial.Value <= 0.2)
        {
            condenser.FillContainer("Water", material);
        } else
        {
            condenser.EmptyContainer();
        }  
    }

    public bool IsReady()
    {
        return condenser.IsFull();
    }
}
