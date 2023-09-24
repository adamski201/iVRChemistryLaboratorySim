using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
#pragma warning disable CS0252 // Possible unintended reference comparison; left hand side needs cast

public class VialController : MonoBehaviour
{
    /* Handles anti-bumping granule vial behaviour. Specifically, removes the ability of 
     * the player to pick up the vial by the lid which was causing unintended behaviour. */

    // Initializes the collider which is used to grab the lid
    [SerializeField] private Collider vialGrabCollider;

    // holds the hidden collider that acts as the /physics/ lid.
    [SerializeField] private Collider granuleGrabCollider;

    // Link to the granule object to multiply
    [SerializeField] private GameObject granuleObject;

    // How many granule objects we want.
    [SerializeField] private int granuleCount = 8;

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

        // create granuleCount-1 duplicates of granuleObject
        for (int i = 0; i < granuleCount; i++)
            Object.Instantiate(granuleObject, granuleObject.transform.parent);

        // Sets vial to listen to the SelectEnter and SelectExit events
        //vial.selectEntered.AddListener(onSelectEnter);
        //vial.selectExited.AddListener(onSelectExit);
    }

    // When the vial is picked up, the vial's lid can now be grabbed and removed
    public void onSelectEnter(SelectEnterEventArgs args)
    {
        Debug.Log("Vial on select enter");
        if (args.interactorObject == leftController || args.interactorObject == rightController)
        {
            vialGrabCollider.gameObject.SetActive(true);
        }
    }
    
    // When the vial is dropped, if the lid is attached the lid's grab collider is disabled
    public void onSelectExit(SelectExitEventArgs args)
    {
        Debug.Log("Vial on select exit");
        if (socket.hasSelection && (args.interactorObject == leftController || args.interactorObject == rightController))
        {
            vialGrabCollider.gameObject.SetActive(false);
        }
    }

    public void updateGranuleCollider()
    {
        granuleGrabCollider.gameObject.SetActive(socket.hasSelection);
    }
}
#pragma warning enable CS0252 // Possible unintended reference comparison; left hand side needs cast
