using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeDistanceLimit : MonoBehaviour
{
    [SerializeField] private GameObject origin;
    [SerializeField] private float maxDistance;

    void Update()
    {
        limitDistance();
    }

    void limitDistance()
    {
        Vector3 direction = transform.position - origin.transform.position;
        float distance = direction.sqrMagnitude;

        if (distance > maxDistance * maxDistance)
        {
            transform.position = direction.normalized * maxDistance + origin.transform.position;
        }
    }
}
