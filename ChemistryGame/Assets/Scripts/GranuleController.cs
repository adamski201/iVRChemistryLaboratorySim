using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GranuleController : MonoBehaviour
{
    // This class handles anti-bumping granule behaviour
    [SerializeField] private UnityEvent solidAddedTrigger;
    [SerializeField] private AudioSource granuleSFX;

    // Communicates that a granule has been added to the flask
    private void OnTriggerEnter(Collider other)
    {
        // has hit the collider at the top of the flask
        if(other.CompareTag("SolidEntryPoint"))
        {
            //Destroy(gameObject);
            //Moves granules to GranuleHolder box in heirarchy and physical position
            Transform newParent = other.transform.parent.Find("GranuleHolder");
            gameObject.transform.SetParent(newParent);
            // Randomize where it instantiates
            gameObject.transform.localPosition = 0.005f * Random.insideUnitSphere;
            
            gameObject.transform.rotation = new Quaternion();
            gameObject.layer = LayerMask.NameToLayer("Granules");
            granuleSFX.Play();
        }
    }
}
