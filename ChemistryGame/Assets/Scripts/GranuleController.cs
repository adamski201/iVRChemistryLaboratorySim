using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GranuleController : MonoBehaviour
{
    // This class handles anti-bumping granule behaviour
    [SerializeField] private UnityEvent solidAddedTrigger;

    // Communicates that a granule has been added to the flask
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("SolidEntryPoint"))
        {
            Destroy(gameObject);
            solidAddedTrigger.Invoke();
        }
    }
}
