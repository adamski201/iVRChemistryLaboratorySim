using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorController : MonoBehaviour
{
    [SerializeField] private GameObject waterTubesError;
    [SerializeField] private GameObject bumpingError;
    [SerializeField] private GameObject wiresError;
    [SerializeField] private GameObject clampError;
    [SerializeField] private GameObject heatError;
    [SerializeField] private GameObject stopperError;
    private AudioSource audioSource;
    private bool errorActive;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ShowWaterTubeError()
    {
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
