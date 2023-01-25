using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GranuleController : MonoBehaviour
{
    [SerializeField] private UnityEvent solidAddedTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("SolidEntryPoint"))
        {
            Destroy(gameObject);
            solidAddedTrigger.Invoke();
        }
    }
}
