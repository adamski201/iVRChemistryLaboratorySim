using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourLiquid : MonoBehaviour
{
    public Collider fromCollider;
    public Collider toCollider;
    public float fromLiquidMaxYScale, toLiquidMaxYScale;

    private void Start()
    {

    }

    private void Update()
    {
        Vector3 scaleChange = new(0, 0.001f, 0);
        HideLiquidWhenDepleted();

        if (CollidersAreTouching())
        {
            if (fromCollider.transform.parent.transform.lossyScale.y > 0 && toCollider.transform.parent.transform.lossyScale.y < toLiquidMaxYScale)
            {
                fromCollider.transform.parent.transform.localScale -= scaleChange;
                toCollider.transform.parent.transform.localScale += scaleChange;
            }
        }
    }

    private bool CollidersAreTouching()
    {
        if (fromCollider.bounds.Intersects(toCollider.bounds))
        {
            return true;
        } else
        {
            return false;
        }
    }

    private void HideLiquidWhenDepleted()
    {
        if (fromCollider.transform.parent.transform.lossyScale.y <= 0)
        {
            fromCollider.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            fromCollider.transform.parent.gameObject.SetActive(true);
        }

        if (toCollider.transform.parent.transform.lossyScale.y <= 0)
        {
            toCollider.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            toCollider.transform.parent.gameObject.SetActive(true);
        }
    }

}
