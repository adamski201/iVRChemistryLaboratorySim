using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DistillationController : MonoBehaviour
{
    public XRSocketInteractor retortSocket;
    public XRSocketInteractor furnaceSocket;
    public GameObject fire;
    public LiquidContainer flask;
    public LiquidContainer retort;
    public Material newMaterial;
    public string newLiquid;
    private Vector3 fillScaleChange = new(0, 0.00001f, 0);
    private Vector3 emptyScaleChange = new(0, 0.00001f, 0);

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
            retort.EmptyContainer(emptyScaleChange);
            flask.FillContainer(fillScaleChange, "Black", newMaterial);
            yield return null;
        }
    }
}
