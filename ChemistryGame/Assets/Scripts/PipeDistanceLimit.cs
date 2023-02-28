using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeDistanceLimit : MonoBehaviour
{
    // This class prevents the water tubes from being stretched indefinitely.
    // Essentially prevents a visual bug.
    [SerializeField] private GameObject origin;
    [SerializeField] private float maxDistance;

    void Update()
    {
        limitDistance();
    }

    // Does some maths to figure out the distance that the tube is being stretched and prevents it going further than the limit
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
