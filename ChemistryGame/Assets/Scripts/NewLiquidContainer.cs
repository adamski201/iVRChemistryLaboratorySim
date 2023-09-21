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
    private float lastAmount;
    private Quaternion lastRotation;
    
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
        
        // initialise these to nonsense values.
        lastAmount = -1.0f;
        lastRotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        // If we are tipped over enough, pour out
        if (!IsUpright() && !IsEmpty())
        {
            EmptyContainer();
        }

        // if the amount has changed since last time, or we have tilted the object reset the level
        if ( lastAmount != liquidAmount || lastRotation != transform.rotation)
        {
            liquidScript.SetFillAmount(liquidAmount);
            lastAmount = liquidAmount;
            lastRotation = transform.rotation;
            //lastBoundsY = transform.rotation.y;
        }
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
    }

    public void EmptyContainer(float emptyRate)
    {
        if (IsEmpty()) return;
        //float oldLA = liquidAmount;
        liquidAmount = Mathf.Max(liquidAmount - (emptyRate / flowFactor), 0.0f);
        liquidScript.SetFillAmount(liquidAmount);
        //Debug.LogFormat("{0} going from {1} to {2}", gameObject.name, oldLA, liquidAmount);
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
        if (IsFull())
        {
            //Debug.LogFormat("{0} is full", gameObject.name);
            return;
        }
        //float oldAmount = liquidAmount;
        liquidAmount = Mathf.Clamp(Mathf.Min(liquidAmount + (fillRate / flowFactor), maxLiquidAmount),0,1);
        liquidScript.SetFillAmount(liquidAmount);
        //Debug.LogFormat("{0} going from {1} to {2} full.", gameObject.name, oldAmount, liquidAmount);
    }

    private float getLipPosition()
    {
        return Mathf.Clamp((openingCollider.bounds.min.y - lcRenderer.bounds.min.y) / lcRenderer.bounds.size.y, 0.0f, 1.0f);
    }
}
