using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorController : MonoBehaviour
{
    // This class displays error messages on the whiteboard when an error event is triggered
    // Correction event triggers will hide the associated error

    [SerializeField] private GameObject waterTubesError;
    [SerializeField] private GameObject bumpingError;
    [SerializeField] private GameObject wiresError;
    [SerializeField] private GameObject clampError;
    [SerializeField] private GameObject heatError;
    [SerializeField] private GameObject stopperError;
    private AudioSource audioSource;

    // True when an error is already being displayed on the board
    private bool errorActive;

    // Start is called before the first frame update
    private void Start()
    {
        // Initialize scripts
        audioSource = GetComponent<AudioSource>();
    }

    public void ShowWaterTubeError()
    {
        // Prevents multiple errors being displayed on top of each other
        if (errorActive)
        {
            HideAllErrors();
        }

        waterTubesError.SetActive(true);
        audioSource.Play();
        errorActive = true;
    }

    public void HideWaterTubeError()
    {
        waterTubesError.SetActive(false);
        errorActive = false;
    }

    public void ShowBumpingError()
    {
        if (errorActive)
        {
            HideAllErrors();
        }

        bumpingError.SetActive(true);
        audioSource.Play();
        errorActive = true;
    }

    public void HideBumpingError()
    {
        bumpingError.SetActive(false);
        errorActive = false;
    }

    public void ShowWiresError()
    {
        if (errorActive)
        {
            HideAllErrors();
        }

        wiresError.SetActive(true);
        audioSource.Play();
        errorActive = true;
    }

    public void HideWiresError()
    {
        wiresError.SetActive(false);
        errorActive = false;
    }

    public void ShowClampError()
    {
        if (errorActive)
        {
            HideAllErrors();
        }

        clampError.SetActive(true);
        audioSource.Play();
        errorActive = true;
    }

    public void HideClampError()
    {
        clampError.SetActive(false);
        errorActive = false;
    }

    public void ShowHeatError()
    {
        if (errorActive)
        {
            HideAllErrors();
        }

        heatError.SetActive(true);
        audioSource.Play();
        errorActive = true;
    }

    public void HideHeatError()
    {
        heatError.SetActive(false);
        errorActive = false;
    }

    public void ShowStopperError()
    {
        if (errorActive)
        {
            HideAllErrors();
        }

        stopperError.SetActive(true);
        audioSource.Play();
        errorActive = true;
    }

    public void HideStopperError()
    {
        stopperError.SetActive(false);
        errorActive = false;
    }

    private void HideAllErrors()
    {
        stopperError.SetActive(false);
        heatError.SetActive(false);
        clampError.SetActive(false);
        wiresError.SetActive(false);
        bumpingError.SetActive(false);
        waterTubesError.SetActive(false);

        errorActive = false;
    }
}
