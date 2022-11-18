using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LightFire : MonoBehaviour
{
    public XRSocketInteractor socket1;
    public XRSocketInteractor socket2;
    public XRSocketInteractor socket3;
    public GameObject fire;

    private void Awake()
    {
        fire.SetActive(false);
    }

    private void Update()
    {
        if (FilledWithCoal())
        {
            fire.SetActive(true);
        } else
        {
            fire.SetActive(false);
        }
    }

   private bool FilledWithCoal()
    {
        return socket1.hasSelection && socket2.hasSelection && socket3.hasSelection;
    }
}
