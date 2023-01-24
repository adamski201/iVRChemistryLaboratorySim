using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class CondenserController : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor intake;
    [SerializeField] private XRSocketInteractor outtake;
    [SerializeField] private DialInteractable dial;
    [SerializeField] private LiquidContainer condenser;
    [SerializeField] private Vector3 scaleChange = new(0, 0.000001f, 0);
    [SerializeField] private Material material;

    // Update is called once per frame
    void Update()
    {
        if (intake.hasSelection && outtake.hasSelection && dial.Value <= 0.2)
        {
            condenser.FillContainer(scaleChange, "Water", material);
        } else
        {
            condenser.EmptyContainer(scaleChange);
        }
    }

    
}
