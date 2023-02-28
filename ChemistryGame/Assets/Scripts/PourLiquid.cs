using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourLiquid : MonoBehaviour
{
    // Handles liquid pouring behaviour. Ideally will be refactored into the LiquidContainer class
    // if I had more time/wanted to expand the project

    public GameObject liquid;
    private LiquidContainer thisContainer;
    private MeshRenderer mesh;

    private void Start()
    {
        // Initialize objects
        thisContainer = gameObject.GetComponent<LiquidContainer>();
        mesh = liquid.GetComponent<MeshRenderer>();
    }

    // Fills receiver container when the colliders are intersecting
    private void OnTriggerStay(Collider other)
    {   
        if (other.CompareTag("PourPoint") && !thisContainer.IsEmpty())
        {
            LiquidContainer receiverContainer = other.transform.parent.GetComponent<LiquidContainer>();

            if (!receiverContainer.IsFull())
            {
                receiverContainer.FillContainer(liquid.name, mesh.material);
                thisContainer.EmptyContainer();
            }
            
        }
    }
}
