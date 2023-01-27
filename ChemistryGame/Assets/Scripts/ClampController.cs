using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class ClampController : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor correctSocket;
    public bool isCorrect;

    private void Update()
    {
        CheckSocket();
    }

    private void CheckSocket()
    {
        if (correctSocket.hasSelection)
        {
            isCorrect = true;
        } else
        {
            isCorrect = false;
        }
    }
}
