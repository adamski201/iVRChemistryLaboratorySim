using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class NewLiquidContainer : MonoBehaviour
{
    public const string POURTAG = "PourPoint";

    // The current proportion of liquid in the container (0.0-1.0)
    public float liquidAmount = 0.0f;

    // A value representing (roughly) the cross sectional area of the container.
    public float flowFactor = 1.0f;

    // Default rate (if none given) for liquird to flow in/out of the container. Modified by flowFactor
    public float defaultFlowRate = 0.001f;

    public float maxLiquidAmount = 0.9f;
    // which way is up. #TODO make this a vector?
    public string upAxis = "z";

    // The object we are filling/empting. #TODO Can we move this to the object and introspect?
    public Liquid liquidScript;

    // Can this object produce infinite amounts of liquid.
    public bool infiniteLiquid = false;

    // Where does the liquid come from?
    public GameObject opening;

    private Collider openingCollider;
    private Renderer lcRenderer;
    private Ray pourRay;
    
    // TODO:: Implement pouring different liquids.

    // Start is called before the first frame update
    void Start()
    {
        liquidScript.SetFillAmount(liquidAmount);
        openingCollider = opening.GetComponent<Collider>();
        if (openingCollider == null)
            throw new Exception("Opening object lacks collider");
        lcRenderer = GetComponent<Renderer>();
        if (lcRenderer == null)
            throw new Exception("Liquid Container lacks renderer");
        pourRay = new Ray(openingCollider.bounds.min, new Vector3(0, -1, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsUpright() && !IsEmpty())
        {
            EmptyContainer();
        }
        liquidScript.SetFillAmount(liquidAmount);
    }

    public bool IsFull()
    {
        return liquidAmount >= maxLiquidAmount;
    }

    public bool IsEmpty()
    {
        return liquidAmount <= 0.0f || infiniteLiquid;
    }
    


    private bool IsUpright()
    {
        return getLipPosition() >= liquidAmount;
    }

    public void EmptyContainer()
    {
        EmptyContainer(defaultFlowRate);
    }

    public void EmptyContainer(float emptyRate)
    {
        if (IsEmpty()) return;
        liquidAmount = Mathf.Max(liquidAmount - (emptyRate / flowFactor), 0.0f);
        liquidScript.SetFillAmount(liquidAmount);

        NewLiquidContainer otherLC;

        pourRay.origin = openingCollider.bounds.min;
        RaycastHit[] raycastHits = Physics.RaycastAll(pourRay);
        foreach (RaycastHit raycastHit in raycastHits)
        {
            if (raycastHit.transform == null) continue;
            GameObject hitObject = raycastHit.transform.gameObject;
            otherLC = hitObject.GetComponent<NewLiquidContainer>();
            if (otherLC == null)
            {
                otherLC = GetComponentInParent<NewLiquidContainer>();
            }
            // TODO:: I think this means we can pour through a desk. Oops.
            if (otherLC != null && otherLC != this)
            {
                otherLC.FillContainer(emptyRate);
                return;
            }
        }
    }



    public void FillContainer()
    {
        FillContainer(defaultFlowRate);
    }

    public void FillContainer(float fillRate) { 
        if (IsFull()) return;
        liquidAmount = Mathf.Min(liquidAmount + (fillRate / flowFactor), maxLiquidAmount);
        liquidScript.SetFillAmount(liquidAmount);
    }

    private float getLipPosition()
    {
        return Mathf.Clamp((openingCollider.bounds.min.y - lcRenderer.bounds.min.y) / lcRenderer.bounds.size.y, 0.0f, 1.0f);
    }
}
