using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VialController : MonoBehaviour
{
    [SerializeField] private Collider vialGrabCollider;
    [SerializeField] private XRGrabInteractable vial;
    [SerializeField] private XRSocketInteractor socket;
    [SerializeField] XRDirectInteractor leftController;
    [SerializeField] XRDirectInteractor rightController;

    private void Start()
    {
        vialGrabCollider.gameObject.SetActive(false);
        vial.selectEntered.AddListener(onSelectEnter);
        vial.selectExited.AddListener(onSelectExit);
    }

    private void onSelectEnter(SelectEnterEventArgs args)
    {
        if (args.interactorObject == leftController || args.interactorObject == rightController)
        {
            vialGrabCollider.gameObject.SetActive(true);
        }
    }

    private void onSelectExit(SelectExitEventArgs args)
    {
        if (socket.hasSelection && (args.interactorObject == leftController || args.interactorObject == rightController))
        {
            vialGrabCollider.gameObject.SetActive(false);
        }
    }
}
