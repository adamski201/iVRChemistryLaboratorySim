using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TongsController : MonoBehaviour
{
    // CLASS IS DEPRECATED. Preserved for posterity. This class was part of the project's prototype.

    [SerializeField] private XRSocketInteractor tongSocket;
    private XRGrabInteractable coalObj;

    private void Start()
    {
        tongSocket.socketActive = false;

        tongSocket.hoverEntered.AddListener(OnHoverEnter);
        tongSocket.hoverExited.AddListener(OnHoverExit);
    }

    public void PickUpCoal()
    {
        tongSocket.socketActive = true;
        if (coalObj != null)
        {
            tongSocket.StartManualInteraction(coalObj);
        }

        XRSocketInteractor coalHolder = coalObj.selectingInteractor.GetComponent<XRSocketInteractor>();
        if (coalHolder != null)
        {
            StartCoroutine(DisableSocket(coalHolder));
            tongSocket.StartManualInteraction(coalObj);
        }
    }

    public void DropCoal()
    {
        if (tongSocket.hasSelection)
        {
            tongSocket.EndManualInteraction();
        }

        tongSocket.socketActive = false;
    }

    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        coalObj = args.interactableObject.transform.GetComponent<XRGrabInteractable>();
    }

    private void OnHoverExit(HoverExitEventArgs args)
    {
        coalObj = null;
    }

    IEnumerator DisableSocket(XRSocketInteractor socket)
    {
        socket.socketActive = false;

        yield return new WaitForSeconds(1);

        socket.socketActive = true;

        yield return null;
    }
}
