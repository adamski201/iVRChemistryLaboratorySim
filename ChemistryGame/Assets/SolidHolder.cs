using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidHolder : MonoBehaviour
{
    public AudioClip splash;
    public AudioClip plink;
    public NewLiquidContainer flask;
    public Material granuleDryMaterial;
    public Material granuleWetMaterial;
    public float lastLiquidAmount = float.MaxValue;
    // Start is called before the first frame update
    void Start()
    {
        // TODO:: remove this horrid hack and move the sound playing here.
        GranuleController.splash = splash;
        GranuleController.plink = plink;
        GranuleController.flask = flask;
        GranuleController.wetMaterial = granuleWetMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO:: This should probably be a change event fired by nlc
        if( flask.liquidAmount != lastLiquidAmount )
        {
            lastLiquidAmount = flask.liquidAmount;
            if (lastLiquidAmount > 0.1)
            {
                WetTheDrys();
            } else
            {
                DryTheWets();
            }
        }
    }

    public bool IsEmpty()
    {
        return transform.childCount == 0;
    }

    public void WetTheDrys()
    {
        foreach (Renderer r in transform.GetComponentsInChildren<Renderer>())
            r.material = granuleWetMaterial;
    }

    public void DryTheWets()
    {
        foreach (Renderer r in transform.GetComponentsInChildren<Renderer>())
            r.material = granuleDryMaterial;
    }
}
