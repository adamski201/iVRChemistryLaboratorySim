using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourLiquid : MonoBehaviour
{
    private Vector3 scaleChange = new(0, 0.0005f, 0);
    public GameObject liquid;
    private string liquidName;
    private Material liquidMaterial;

    private void Start()
    {
        liquidName = liquid.name;
        MeshRenderer mesh = liquid.GetComponent<MeshRenderer>();
        liquidMaterial = mesh.material;
    }

    private void OnTriggerStay(Collider other)
    {   
        if (other.CompareTag("PourPoint"))
        {
            LiquidContainer receiverContainer = other.transform.parent.GetComponent<LiquidContainer>();
            receiverContainer.FillContainer(scaleChange, liquidName, liquidMaterial);
        }
    }
}
