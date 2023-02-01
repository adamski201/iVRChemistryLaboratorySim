using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorController : MonoBehaviour
{
    [SerializeField] private GameObject waterTubesError;
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
}
