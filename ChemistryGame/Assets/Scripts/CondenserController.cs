using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class CondenserController : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor intakeSocket;
    [SerializeField] private XRSocketInteractor outtakeSocket;
    [SerializeField] private Transform intakePipe;
    [SerializeField] private Transform outtakePipe;
    [SerializeField] private Collider intakeCollider;
    [SerializeField] private Collider outtakeCollider;
    [SerializeField] private DialInteractable dial;
    [SerializeField] private LiquidContainer condenser;
    [SerializeField] private Material material;
    public bool tubesAttached = false;
    public bool tubesCorrectlyAttached = false;
    public bool isReady = false;

    // Update is called once per frame
    void Update()
    {
        CheckTubesAttached();
        HandleCondenserInternal();
        HandleSockets();

        if (condenser.IsFull())
        {
            isReady = true;
        } else
        {
            isReady = false;
        }

        Debug.Log("outtake" + outtakePipe.localPosition.y + outtakePipe.position.y);
        Debug.Log("intake" + intakePipe.localPosition.y + intakePipe.position.y);
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
        }
        else
        {
            tubesCorrectlyAttached = false;
        }
    }

    private void HandleCondenserInternal()
    {        
        if (tubesCorrectlyAttached && dial.Value <= 0.2)
        {
            condenser.FillContainer("Water", material);
        }
        else if (!tubesCorrectlyAttached && dial.Value <= 0.2)
        {
            Debug.Log("Tubes not attached or incorrectly attached");
            condenser.EmptyContainer();
        } else
        {
            condenser.EmptyContainer();
        }
       
    }
}
