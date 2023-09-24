using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidHolder : MonoBehaviour
{
    public AudioClip splash;
    public AudioClip plink;
    public  NewLiquidContainer flask;
    // Start is called before the first frame update
    void Start()
    {
        // TODO:: remove this horrid hack and move the sound playing here.
        GranuleController.splash = splash;
        GranuleController.plink = plink;
        GranuleController.flask = flask;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsEmpty()
    {
        return transform.childCount == 0;
    }
}
