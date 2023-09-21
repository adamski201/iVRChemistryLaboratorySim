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
        if (liquidScript == null)
        {
            // liquid script not specified. Try and find it.
            liquidScript = GetComponentInChildren<Liquid>();
            if (liquidScript == null)
                throw new Exception(string.Format("Liquid script not found in {0}", gameObject.name));
        } 
        liquidScript.SetFillAmount(liquidAmount);
        openingCollider = opening.GetComponent<Collider>();
        if (openingCollider == null)
            throw new Exception(string.Format("Opening object lacks collider in {0}", gameObject.name));
        lcRenderer = GetComponent<Renderer>();
        if (lcRenderer == null)
            throw new Exception(string.Format("Liquid Container lacks renderer in {0}", gameObject.name));
        pourRay = new Ray(getPourOrigin(), new Vector3(0, -1, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsUpright() && !IsEmpty())
        {
            EmptyContainer();
        }
        // TODO:: wrap in test to see if needed?
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

    private Vector3 getPourOrigin()
    {
        return opening.transform.position;
        //return inCollider.bounds.min;
        /*
        float lowestY = float.MaxValue;
        Vector3 result = Vector3.zero;

        foreach( Vector3 v in opening.GetComponent<MeshFilter>().mesh.vertices)
        {
            Vector3 candidate = transform.TransformPoint(v);

            if( candidate.y < lowestY)
            {
                lowestY = candidate.y;
                result = candidate;
            }
        }

        return result;*/
    }

    public void EmptyContainer(float emptyRate)
    {
        if (IsEmpty()) return;
        float oldLA = liquidAmount;
        liquidAmount = Mathf.Max(liquidAmount - (emptyRate / flowFactor), 0.0f);
        liquidScript.SetFillAmount(liquidAmount);

        NewLiquidContainer otherLC;

        pourRay.origin = getPourOrigin();
        RaycastHit[] raycastHits = Physics.RaycastAll(pourRay,float.MaxValue,-1,QueryTriggerInteraction.Collide);
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
