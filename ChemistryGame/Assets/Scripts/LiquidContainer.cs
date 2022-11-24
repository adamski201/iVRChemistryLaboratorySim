using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidContainer : MonoBehaviour
{
    public float maxLiquidYScale = 0.035f;
    public string upAxis;
    public Transform liquid;
    private Vector3 emptyScaleChange = new(0, 0.001f, 0);

    private void Update()
    {
        HideWhenEmpty();

        if (!IsUpright())
        {
            EmptyContainer();
        }
        
        Debug.Log(transform.forward);
    }

    public void FillContainer(Vector3 scaleChange)
    {
        if (!IsFull())
        {
            liquid.localScale += scaleChange;

            liquid.localPosition += new Vector3(0, 0, scaleChange.y);
        }
    }

    private bool IsFull()
    {
        if (liquid.lossyScale.y >= maxLiquidYScale)
        {
            return true;
        }

        return false;
    }

    private bool IsEmpty()
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

        return true;
    }

    private void EmptyContainer()
    {
        if (!IsEmpty())
        {
            liquid.localScale -= emptyScaleChange;
            liquid.localPosition -= new Vector3(0, 0, emptyScaleChange.y);
        }
    }

    private void HideWhenEmpty()
    {
        if (IsEmpty())
        {
            liquid.gameObject.SetActive(false);
        }
        else
        {
            liquid.gameObject.SetActive(true);
        }
    }
}
