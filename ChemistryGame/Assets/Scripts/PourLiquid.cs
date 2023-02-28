using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourLiquid : MonoBehaviour
{
    // Handles liquid pouring behaviour. Ideally will be refactored into the LiquidContainer class
    // if I had more time/wanted to expand the project

    public GameObject liquid;
    private string liquidName;
    private Material liquidMaterial;
    private LiquidContainer thisContainer;

    private void Start()
    {
        // Initialize scripts and variables
        liquidName = liquid.name;
        thisContainer = gameObject.GetComponent<LiquidContainer>();

        MeshRenderer mesh = liquid.GetComponent<MeshRenderer>();
        liquidMaterial = mesh.material;

    }

    // Fills receiver container when the colliders are intersecting
    private void OnTriggerStay(Collider other)
    {   
        if (other.CompareTag("PourPoint") && !thisContainer.IsEmpty())
        {
            LiquidContainer receiverContainer = other.transform.parent.GetComponent<LiquidContainer>();

            if (!receiverContainer.IsFull())
            {
                receiverContainer.FillContainer(liquidName, liquidMaterial);
                thisContainer.EmptyContainer();
            }
            
        }
    }
}
