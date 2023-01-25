using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RefluxController : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor standSocket;
    [SerializeField] private XRSocketInteractor hotplateSocket;
    [SerializeField] private XRSocketInteractor flaskSocket;
    [SerializeField] private LiquidContainer flask;
    [SerializeField] private LiquidContainer condenser;
    [SerializeField] private DialInteractable heatDial;
    [SerializeField] private ParticleSystem boilingEffect;

    private void Update()
    {
        if (ReactionIsReady())
        {
            boilingEffect.gameObject.SetActive(true);
        } else
        {
            boilingEffect.gameObject.SetActive(false);
        }
    }

    private bool ReactionIsReady()
    {
        return standSocket.hasSelection &&
               hotplateSocket.hasSelection &&
               flaskSocket.hasSelection &&
               flask.containsAntiBumpGranules &&
               condenser.IsFull() &&
               flask.IsFull() &&
               heatDial.Value <= 0.5 &&
               heatDial.Value >= 0.2;
    }
}
