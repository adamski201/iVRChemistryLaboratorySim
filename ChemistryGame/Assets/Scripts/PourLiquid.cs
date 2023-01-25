using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourLiquid : MonoBehaviour
{
    public GameObject liquid;
    private string liquidName;
    private Material liquidMaterial;
    private LiquidContainer thisContainer;

    private void Start()
    {
        liquidName = liquid.name;
        thisContainer = gameObject.GetComponent<LiquidContainer>();

        MeshRenderer mesh = liquid.GetComponent<MeshRenderer>();
        liquidMaterial = mesh.material;

    }

    private void OnTriggerStay(Collider other)
    {   
        if (other.CompareTag("PourPoint") && !thisContainer.IsEmpty())
        {
            LiquidContainer receiverContainer = other.transform.parent.GetComponent<LiquidContainer>();
            receiverContainer.FillContainer(liquidName, liquidMaterial);
            thisContainer.EmptyContainer();
        }
    }
}
