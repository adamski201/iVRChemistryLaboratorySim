using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

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
    [SerializeField] private GameObject victoryMessage;
    [SerializeField] private GameObject errorBoard;
    private TextMeshPro errorText;
    private MessageStack messageStack = new();

    public AudioSource audioSource;

    private LocalizedString message = new();

    // Start is called before the first frame update
    private void Start()
    {
        // Initialize scripts
        audioSource = GetComponent<AudioSource>();
        errorText = errorBoard.GetComponentInChildren<TextMeshPro>();
        message.TableReference = "Whiteboard Messages";
    }

    public void ShowSuccess()
    {
        AddError("Success Message");

    }

    public void ShowWaterTubeError()
    {
        AddError("Water Error");
    }

    public void HideWaterTubeError()
    {
        RemoveError("Water Error");
    }

    public void ShowBumpingError()
    {
        AddError("Granules Error");
    }

    public void HideBumpingError()
    {
        RemoveError("Granules Error");
    }

    public void ShowWiresError()
    {
        AddError("Wire Error");
    }

    public void HideWiresError()
    {
        RemoveError("Wire Error");
    }

    public void ShowClampError()
    {
        AddError("Clamp Error");
    }

    public void HideClampError()
    {
        RemoveError("Clamp Error");
    }

    public void ShowHeatError()
    {
        AddError("Too Hot Error");
    }
    public void HideHeatError()
    {
        RemoveError("Too Hot Error");
    }

    public void ShowStopperError()
    {
        AddError("Stopper Error");
    }

    public void HideStopperError()
    {
        RemoveError("Stopper Error");
    }

    private void AddError(string errorKey)
    {
        messageStack.Push(errorKey);
        updateBoard();
    }

    private void RemoveError(string errorKey)
    {
        messageStack.Remove(errorKey);
        updateBoard();
    }

    private void updateBoard()
    {
        if( messageStack.Count() > 0 )
        {
            // there are errors to display.
            message.TableEntryReference = messageStack.Peek();
            errorText.text = message.GetLocalizedString();
            errorBoard.SetActive(true);
            audioSource.Play();
        } else
        {
            errorBoard.SetActive(false);
        }
    }
}
