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
        // has hit the collider at the top of the flask
        if(other.CompareTag("SolidEntryPoint"))
        {
            //Destroy(gameObject);
            Transform newParent = other.transform.parent.Find("GranuleHolder");
            gameObject.transform.SetParent(newParent);
            gameObject.transform.localPosition = 0.005f * Random.insideUnitSphere;
            
            gameObject.transform.rotation = new Quaternion();
            gameObject.layer = LayerMask.NameToLayer("Granules");
            solidAddedTrigger.Invoke();
            
        }
    }
}
