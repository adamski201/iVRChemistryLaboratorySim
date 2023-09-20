using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddingGranules : MonoBehaviour
{
    // Is true when granules are inserted into the top of the liquid.
    [HideInInspector] public bool containsAntiBumpGranules;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This is triggered by the GranuleController script
    public void addBumpGranules()
    {
        containsAntiBumpGranules = true;


    }
}
