using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RefluxController : MonoBehaviour
{
    // This class monitors the state of the reflux procedure and indicates success when complete

    [SerializeField] private ClampController clamp;
    [SerializeField] private CondenserController condenser;
    [SerializeField] private FlaskController flask;
    [SerializeField] private DialInteractable heatDial;
    [SerializeField] private ParticleSystem boilingEffect;
    [SerializeField] private AudioSource successSFX;
    [SerializeField] private AudioSource boilingSFX;
    private bool refluxBegun;

    // Update is called once per frame
    private void Update()
    {
        HandleReflux();
    }

    // Handles logic for starting heating under reflux
    private void HandleReflux()
    {
        if (!refluxBegun && ReactionIsReady())
        {
            refluxBegun = true;
            StartCoroutine(Reflux());
        }
        else if (refluxBegun && !ReactionIsReady())
        {
            boilingEffect.gameObject.SetActive(false);
            boilingSFX.Pause();
        }
    }

    // Checks if the apparatus has been set up correctly
    private bool ReactionIsReady()
    {
        return clamp.IsReady() &&
               flask.IsReady() &&
               condenser.IsReady() &&
               heatDial.Value <= 0.5 &&
               heatDial.Value >= 0.2;
    }

    // IEnumerator allows execution over several frames
    IEnumerator Reflux()
    {
        yield return new WaitForSeconds(1.5f);
        successSFX.Play();
        boilingSFX.Play();
        boilingEffect.gameObject.SetActive(true);
    }
}
