using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class WhiteboardMessageController : MonoBehaviour
{
    // This class displays error messages on the whiteboard when an error event is triggered
    // Correction event triggers will hide the associated error

    [SerializeField] private GameObject errorBoard;
    // a symbol shown on the whiteboard (e.g. warning)
    [SerializeField] private GameObject symbol;

    private TextMeshPro messageText;
    private MessageStack messageStack = new();

    public AudioSource audioSource;

    private LocalizedString message = new();

    // Start is called before the first frame update
    private void Start()
    {
        // Initialize scripts
        audioSource = GetComponent<AudioSource>();
        messageText = errorBoard.GetComponentInChildren<TextMeshPro>();
        message.TableReference = "Whiteboard Messages";
        message.TableEntryReference = WhiteboardMessage.WELCOME.Key();
        messageText.text = message.GetLocalizedString();
        errorBoard.SetActive(true);
        symbol.SetActive(false);
    }

    public void RaiseMessage( WhiteboardMessage wm)
    {
        messageStack.Push(wm);
        updateBoard();

    }

    public void RemoveMessage( WhiteboardMessage wm)
    {
        messageStack.Remove(wm);
        updateBoard();

    }
    private void updateBoard()
    {
        if( messageStack.Count() > 0 )
        {
            // there are errors to display.
            WhiteboardMessage wm = messageStack.Peek();
            message.TableEntryReference = wm.Key();
            messageText.text = message.GetLocalizedString();
            errorBoard.SetActive(true);
            if (wm.IsError())
            {
                symbol.SetActive(true);
                audioSource.Play();
            } else
            {
                symbol.SetActive(false);
            } 
        } else
        {
            errorBoard.SetActive(false);
        }
    }
}
