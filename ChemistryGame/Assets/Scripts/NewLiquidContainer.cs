using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLiquidContainer : MonoBehaviour
{
    public const string POURTAG = "PourPoint";

    // The current proportion of liquid in the container (0.0-1.0)
    public float liquidAmount = 0.0f;

    // The rate at which the container empties, per tick
    public float emptyScaleChange = 0.001f;

    // The rate at which the container fills up, per tick
    public float fillScaleChange = 0.005f;

    public float maxLiquidAmount = 0.9f;
    // which way is up. #TODO make this a vector?
    public string upAxis = "z";

    // The object we are filling/empting. #TODO Can we move this to the object and introspect?
    public Liquid liquidScript;

    // Can this object produce infinite amounts of liquid.
    public bool infiniteLiquid = false;

    // TODO:: Implement pouring different liquids.

    // Start is called before the first frame update
    void Start()
    {
        liquidScript.SetFillAmount(liquidAmount);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsUpright())
        {
            EmptyContainer();
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
        // TODO Allow defining how much tilt results in pouring? Should fillAmount figure in?
        if (upAxis == "z" && transform.forward.y < -0.5) return false;
        if (upAxis == "y" && transform.up.y < -0.5) return false;
        return true;
    }

    public void EmptyContainer()
    {
        EmptyContainer(emptyScaleChange);
    }

    public void EmptyContainer(float emptyRate)
    {
        if (IsEmpty()) return;
        liquidAmount = Mathf.Max(liquidAmount - emptyRate, 0.0f);
        liquidScript.SetFillAmount(liquidAmount);
    }



    public void FillContainer()
    {
        FillContainer(fillScaleChange);
    }

    public void FillContainer(float fillRate) { 
        if (IsFull()) return;
        liquidAmount = Mathf.Min(liquidAmount + fillRate, maxLiquidAmount);
        liquidScript.SetFillAmount(liquidAmount);
    }

    private void OnTriggerStay(Collider other)
    {
        // check it is something we can pour into
        if (!other.CompareTag(POURTAG) || IsEmpty()) return;
        
        // find what we are pouring into
        NewLiquidContainer target = other.transform.parent.GetComponent<NewLiquidContainer>();
        if (target == null)
        {
            Debug.LogError("Target object has a pour tag but no Liquid Container?");
            return;
        }

        if( !target.IsFull() )
        {
            // TODO Should this be based on our pour rate?
            float oldAmount = target.liquidAmount;
            target.FillContainer();
            Debug.LogFormat("Target gone from {0} to {1}", oldAmount, target.liquidAmount);
            EmptyContainer();
        }
        
    }
}
