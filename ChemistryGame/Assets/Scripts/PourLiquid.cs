using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourLiquid : MonoBehaviour
{
    public Collider fromCollider;
    public Collider toCollider;
    public float fromLiquidMaxYScale, toLiquidMaxYScale;

    private void Update()
    {
        if (CollidersAreTouching())
        {
            Debug.Log(fromCollider.transform.parent.transform.localScale);
            Vector3 scaleChange = new(0, 0.001f, 0);

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

}
