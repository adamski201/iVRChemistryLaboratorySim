using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketMonitor : MonoBehaviour
{
    [SerializeField] XRSocketInteractor socket;
    [SerializeField] XRGrabInteractable interactable;
    [SerializeField] XRDirectInteractor controller;
    private bool selectedByPlayer = false;

    private void Start()
    {
        socket.socketActive = false;

        interactable.selectEntered.AddListener(onSelectEnter);
        interactable.selectExited.AddListener(onSelectExit);
        socket.hoverEntered.AddListener(onHoverEnter);
    }

    private void onSelectEnter(SelectEnterEventArgs args)
    {
        if (args.interactorObject == controller)
        {
            selectedByPlayer = true;
            socket.socketActive = true;
        }
    }

    private void onSelectExit(SelectExitEventArgs args)
    {
        if (args.interactorObject == controller)
        {
            selectedByPlayer = false;
        }
    }

    private void onHoverEnter(HoverEnterEventArgs args)
    {
        if (args.interactableObject == interactable && selectedByPlayer)
        {
            socket.socketActive = true;
        } else if (args.interactableObject == interactable && !selectedByPlayer)
        {
            socket.socketActive = false;
        }
    }
}
