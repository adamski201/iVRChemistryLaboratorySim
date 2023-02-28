using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LightFire : MonoBehaviour
{
    // CLASS IS DEPRECATED. Preserved for posterity. This class was part of the project's prototype.

    public XRSocketInteractor socket1;
    public XRSocketInteractor socket2;
    public XRSocketInteractor socket3;
    public GameObject weakFire;
    public GameObject mediumFire;
    public GameObject strongFire;
    private int lastCoalCount = 0;

    private void Awake()
    {
        weakFire.SetActive(false);
        mediumFire.SetActive(false);
        strongFire.SetActive(false);
    }

    private void Update()
    {
        HandleFire();
    }

    private int CountCoal()
    {
        return (socket1.hasSelection ? 1 : 0) + (socket2.hasSelection ? 1 : 0) + (socket3.hasSelection ? 1 : 0);
    }

    private void HandleFire()
    {
        int fireStrength = CountCoal();

        if (fireStrength != lastCoalCount)
        {
            switch (fireStrength)
            {
                case 1:
                    weakFire.SetActive(true);
                    mediumFire.SetActive(false);
                    strongFire.SetActive(false);
                    lastCoalCount = 1;
                    break;

                case 2:
                    mediumFire.SetActive(true);
                    weakFire.SetActive(false);
                    strongFire.SetActive(false);
                    lastCoalCount = 2;
                    break;

                case 3:
                    strongFire.SetActive(true);
                    weakFire.SetActive(false);
                    mediumFire.SetActive(false);
                    lastCoalCount = 3;
                    break;

                default:
                    weakFire.SetActive(false);
                    mediumFire.SetActive(false);
                    strongFire.SetActive(false);
                    lastCoalCount = 0;
                    break;
            }
        }
    }
}
