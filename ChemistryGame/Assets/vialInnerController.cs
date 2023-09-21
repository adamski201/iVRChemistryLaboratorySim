using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class vialInnerController : MonoBehaviour
{
    public Collider granuleGrabCollider;

    [SerializeField] private XRGrabInteractable innerVial;
    // Initializes the socket on the vial, which attaches the lid
    [SerializeField] private XRSocketInteractor socket;

    // Start is called before the first frame update
    void Start()
    {
        innerVial.selectEntered.AddListener(onSelectEnter);
        innerVial.selectExited.AddListener(onSelectExit);
    }

    private void onSelectEnter(SelectEnterEventArgs args)
    {
        granuleGrabCollider.gameObject.SetActive(socket.hasSelection);
    }

    private void onSelectExit(SelectExitEventArgs args)
    {
        granuleGrabCollider.gameObject.SetActive(socket.hasSelection);
    }
}
