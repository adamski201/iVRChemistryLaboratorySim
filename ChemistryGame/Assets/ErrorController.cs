using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorController : MonoBehaviour
{
    [SerializeField] private GameObject waterTubesError;
    [SerializeField] private GameObject bumpingError;
    [SerializeField] private GameObject wiresError;
    [SerializeField] private GameObject clampError;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ShowWaterTubeError()
    {
        waterTubesError.SetActive(true);
        audioSource.Play();
    }

    public void HideWaterTubeError()
    {
        waterTubesError.SetActive(false);
    }

    public void ShowBumpingError()
    {
        bumpingError.SetActive(true);
        audioSource.Play();
    }

    public void HideBumpingError()
    {
        bumpingError.SetActive(false);
    }

    public void ShowWiresError()
    {
        wiresError.SetActive(true);
        audioSource.Play();
    }

    public void HideWiresError()
    {
        wiresError.SetActive(false);
    }

    public void ShowClampError()
    {
        clampError.SetActive(true);
        audioSource.Play();
    }

    public void HideClampError()
    {
        clampError.SetActive(false);
    }
}
