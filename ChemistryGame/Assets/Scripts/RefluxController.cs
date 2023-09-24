using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RefluxController : MonoBehaviour
{
    // This is basically the gameplay logic
    // This class monitors the state of the reflux procedure and indicates success when complete

    [SerializeField] private ClampController clamp;
    [SerializeField] private CondenserController condenser;
    [SerializeField] private FlaskController flask;
    [SerializeField] private HeatDialController heatDial;
    [SerializeField] private ParticleSystem boilingEffect;
    [SerializeField] private ParticleSystem boilingEffect1;
    [SerializeField] private ParticleSystem boilingEffectBubbles;
    [SerializeField] private AudioSource successSFX;
    [SerializeField] private AudioSource boilingSFX;
    [SerializeField] private WhiteboardMessageController whiteboard;
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
        //else if (refluxBegun && !ReactionIsReady())
        //{
        //    boilingEffect.gameObject.SetActive(false);
        //    boilingSFX.Pause();
        //}
    }

    // Checks if the apparatus has been set up correctly
    private bool ReactionIsReady()
    {
        return clamp.IsReady() &&
               flask.IsReady() &&
               condenser.IsReady() &&
               heatDial.IsReady();
    }

    // IEnumerator allows execution over several frames
    IEnumerator Reflux()
    {
        yield return new WaitForSeconds(0.5f);
        boilingSFX.Play();
        boilingEffect.gameObject.SetActive(true);
        boilingEffect1.gameObject.SetActive(true);
        boilingEffectBubbles.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        successSFX.Play();
        whiteboard.RaiseMessage(WhiteboardMessage.SUCCESS);
    }
}
