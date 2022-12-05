using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerLinearLimit : MonoBehaviour
{
    [SerializeField] private float localZLimit = 0.35f;

    // Update is called once per frame
    void Update()
    {
        HandleLinearLimit();
    }

    private void HandleLinearLimit()
    {
        if (transform.localPosition.z > localZLimit)
        {
            transform.localPosition = new(transform.localPosition.x, transform.localPosition.y, localZLimit);
        }
    }
}
