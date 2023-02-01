using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RefluxController : MonoBehaviour
{
    [SerializeField] private ClampController clamp;
    [SerializeField] private CondenserController condenser;
    [SerializeField] private FlaskController flask;
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
        return clamp.IsReady() &&
               flask.IsReady() &&
               condenser.IsReady() &&
               heatDial.Value <= 0.5 &&
               heatDial.Value >= 0.2;
    }
}
