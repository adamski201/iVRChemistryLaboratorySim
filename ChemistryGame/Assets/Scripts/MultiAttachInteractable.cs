using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MultiAttachInteractable : MonoBehaviour
{
    public Transform[] attachPoints;
    private InteractionLayerMask grabMask;
    private InteractionLayerMask ourMask;

    public void Awake()
    {
        grabMask = InteractionLayerMask.GetMask("Grabbable");
        ourMask = GetComponentInParent<XRGrabInteractable>().interactionLayers;
    }

    public void HoverOver(HoverEnterEventArgs args)
    {
        
        if (args.interactorObject.GetType().IsEquivalentTo(typeof(XRSocketInteractor)))
        {
            float nearestDistance = 0;
            float candidateDistance;
            Transform nearestTransform = null;
            foreach (Transform attachPoint in attachPoints)
            {
                candidateDistance = (attachPoint.position - args.interactorObject.transform.position).sqrMagnitude;
                if (!nearestTransform || candidateDistance < nearestDistance)
                {
                    nearestTransform = attachPoint;
                    nearestDistance = candidateDistance;
                }
            }

            // TODO:: Assert nearest transform is not null;
            GetComponentInParent<XRGrabInteractable>().attachTransform = nearestTransform;
        } else
        {
            GetComponentInParent<XRGrabInteractable>().attachTransform = null;
        }
    }
  
    public void HoverOut(HoverExitEventArgs args)
    {
        Debug.Log(string.Format("HoverOut {0}", args.interactorObject.ToString()));
        // what have we been grabbed by?

    }

    public void SelectOn(SelectEnterEventArgs args)
    {
        Debug.Log(string.Format("SelectEnter {0}", args.interactorObject.ToString()));
        if (args.interactorObject.GetType().IsEquivalentTo(typeof(XRSocketInteractor)))
        {
            GetComponentInParent<XRGrabInteractable>().interactionLayers = grabMask;
        }
    }

    public void SelectOut(SelectExitEventArgs args)
    {
        Debug.Log(string.Format("SelectExit {0}", args.interactorObject.ToString()));
        if (args.interactorObject.GetType().IsEquivalentTo(typeof(XRSocketInteractor)))
        {          
            GetComponentInParent<XRGrabInteractable>().interactionLayers = ourMask;
        }
    }
}
