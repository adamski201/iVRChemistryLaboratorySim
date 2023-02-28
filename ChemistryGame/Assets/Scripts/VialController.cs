using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VialController : MonoBehaviour
{
    /* Handles anti-bumping granule vial behaviour. Specifically, removes the ability of 
     * the player to pick up the vial by the lid which was causing unintended behaviour. */

    // Initializes the collider which is used to grab the lid
    [SerializeField] private Collider vialGrabCollider;

    // Initializes the vial XRGrabInteractable object
    [SerializeField] private XRGrabInteractable vial;

    // Initializes the socket on the vial, which attaches the lid
    [SerializeField] private XRSocketInteractor socket;

    // Represents the player's controllers
    [SerializeField] XRDirectInteractor leftController;
    [SerializeField] XRDirectInteractor rightController;

    // Start is called before the first frame update
    private void Start()
    {
        // Disables the lid's grab collider initially
        vialGrabCollider.gameObject.SetActive(false);

        // Sets vial to listen to the SelectEnter and SelectExit events
        vial.selectEntered.AddListener(onSelectEnter);
        vial.selectExited.AddListener(onSelectExit);
    }

    // When the vial is picked up, the vial's lid can now be grabbed and removed
    private void onSelectEnter(SelectEnterEventArgs args)
    {
        if (args.interactorObject == leftController || args.interactorObject == rightController)
        {
            vialGrabCollider.gameObject.SetActive(true);
        }
    }
    
    // When the vial is dropped, if the lid is attached the lid's grab collider is disabled
    private void onSelectExit(SelectExitEventArgs args)
    {
        if (socket.hasSelection && (args.interactorObject == leftController || args.interactorObject == rightController))
        {
            vialGrabCollider.gameObject.SetActive(false);
        }
    }
}
