using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DistillationController : MonoBehaviour
{
    // CLASS IS DEPRECATED. Preserved for posterity. This class was part of the project's prototype.

    public XRSocketInteractor retortSocket;
    public XRSocketInteractor furnaceSocket;
    public GameObject fire;
    public LiquidContainer flask;
    public LiquidContainer retort;
    public Material newMaterial;
    public string newLiquid;

    private void Update()
    {
        if (ReactionIsReady())
        {
            StartCoroutine(Distill());
        }
    }

    private bool ReactionIsReady()
    {
        return retortSocket.hasSelection &&
               furnaceSocket.hasSelection &&
               fire.activeSelf &&
               retort.IsFull() &&
               retort.fluidName == "Green" &&
               flask.IsEmpty() &&
               flask.fluidName == null;
    }

    IEnumerator Distill()
    {
        yield return new WaitForSeconds(1);

        while (retort.GetYScale() > 0.025f && flask.GetYScale() < 0.010f)
        {
            retort.EmptyContainer();
            flask.FillContainer("Black", newMaterial);
            yield return null;
        }
    }
}
