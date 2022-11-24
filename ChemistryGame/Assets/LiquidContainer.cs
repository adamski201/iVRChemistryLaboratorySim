using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidContainer : MonoBehaviour
{
    public float maxLiquidYScale = 0.035f;
    private Transform liquid;

    private void Start()
    {
        liquid = transform.Find("Liquid");
    }

    private void Update()
    {
        Debug.Log(transform.up.z);
    }

    public void FillUp(Vector3 scaleChange)
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
        else
        {
            return false;
        }
    }

    private bool IsEmpty()
    {
        if (liquid.lossyScale.y <= 0)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
