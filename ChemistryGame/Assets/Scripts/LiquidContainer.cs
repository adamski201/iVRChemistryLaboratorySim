using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidContainer : MonoBehaviour
{
    public float maxLiquidYScale = 0.035f;
    public string upAxis;
    public Transform liquid;
    public string fluidName;
    public bool isInfinite;
    private Vector3 emptyScaleChange = new(0, 0.001f, 0);
    private MeshRenderer liquidMesh;

    private void Start()
    {
        liquidMesh = liquid.GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        ResetWhenEmpty();

        if (!IsUpright())
        {
            EmptyContainer(emptyScaleChange);
        }        
    }

    public void FillContainer(Vector3 scaleChange, string newliquidName, Material newliquidMaterial)
    {
        if (IsEmpty())
        {
            fluidName = newliquidName;
            liquidMesh.material = newliquidMaterial;
        }

        if (fluidName == newliquidName)
        {
            if (!IsFull())
            {
                liquid.localScale += scaleChange;

                if (upAxis == "z")
                {
                    liquid.localPosition += new Vector3(0, 0, scaleChange.y);
                } else if (upAxis == "y")
                {
                    liquid.localPosition += new Vector3(0, scaleChange.y, 0);
                }
            }
        }
    }

    public bool IsFull()
    {
        if (liquid.lossyScale.y >= maxLiquidYScale)
        {
            return true;
        }

        return false;
    }

    public bool IsEmpty()
    {
        if (liquid.lossyScale.y <= 0)
        {
            return true;
        }

        return false;
    }

    private bool IsUpright()
    {
        if (upAxis == "z")
        {
            if (transform.forward.y < -0.5)
            {
                return false;
            }
        }

        if (upAxis == "y")
        {
            if (transform.up.y < -0.5)
            {
                return false;
            }
        }

        return true;
    }

    public float GetYScale()
    {
        return liquid.lossyScale.y;
    }

    public void EmptyContainer(Vector3 scaleChange)
    {
        if (!isInfinite)
        {
            if (!IsEmpty())
            {
                liquid.localScale -= scaleChange;

                if (upAxis == "z")
                {
                    liquid.localPosition -= new Vector3(0, 0, scaleChange.y);
                } else if (upAxis == "y")
                {
                    liquid.localPosition -= new Vector3(0, scaleChange.y, 0);
                }
            }
        }
    }

    private void ResetWhenEmpty()
    {
        if (IsEmpty())
        {
            liquid.gameObject.SetActive(false);
            fluidName = null;
        }
        else
        {
            liquid.gameObject.SetActive(true);
        }
    }
}
