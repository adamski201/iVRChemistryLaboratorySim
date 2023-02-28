using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class CondenserController : MonoBehaviour
{
    // The CondenserController class handles functions related to the condenser GameObject 

    // Initialize sockets representing the water intake/outtake points on the condenser
    [SerializeField] private XRSocketInteractor intakeSocket;
    [SerializeField] private XRSocketInteractor outtakeSocket;

    // Initializes transforms of the connection points on the water tubes (intake goes to sink, outtake is tap)
    [SerializeField] private Transform intakePipe;
    [SerializeField] private Transform outtakePipe;

    // Initializes colliders of the end points of the water tubes
    [SerializeField] private Collider intakeCollider;
    [SerializeField] private Collider outtakeCollider;

    // Initializes DialInteractable script on the water flow dial
    [SerializeField] private DialInteractable dial;

    // Initializes material that the condenser will fill with (water)
    [SerializeField] private Material material;

    // Initializes Unity Events, which communicate when a mistake has been made and when it has been corrected
    [SerializeField] private UnityEvent incorrectTrigger;
    [SerializeField] private UnityEvent correctTrigger;

    // Ensures that the Unity Events are triggered only once
    private bool triggered = false;

    // Checks that tubes have been attached to condenser (either correctly or incorrectly)
    private bool tubesAttached = false;

    // Checks tubes have been attached only correctly
    private bool tubesCorrectlyAttached = false;

    private AudioSource audioSource;
    private LiquidContainer condenser;

    // -----------------FUNCTIONS----------------------------------------------------------------------------------------------------

    // Start is called before the first frame update
    private void Start()
    {
        // Initialize scripts
        condenser = GetComponent<LiquidContainer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckTubesAttached();
        HandleCondenserInternal();
        HandleSockets();
    }

    // Prevents the tubes from being pulled off the condenser while the water is on
    private void HandleSockets()
    {
        if (dial.Value < 0.2 && tubesAttached)
        {
            intakeCollider.gameObject.SetActive(false);
            outtakeCollider.gameObject.SetActive(false);
        } else
        {
            intakeCollider.gameObject.SetActive(true);
            outtakeCollider.gameObject.SetActive(true);
        }
    }

    // Monitors the state of the condenser
    private void CheckTubesAttached()
    {
        // If both tubes are attached, sets tubesAttached to true
        if (intakeSocket.hasSelection && outtakeSocket.hasSelection)
        {
            tubesAttached = true;
        }
        else
        {
            tubesAttached = false;
        }

        // Uses the positions of the tubes to determine if they are correctly attached
        if (tubesAttached && outtakePipe.localPosition.y < intakePipe.localPosition.y)
        {
            tubesCorrectlyAttached = true;
            if (triggered)
            {
                correctTrigger.Invoke();
                triggered = false;
            }
        }
        else if (tubesAttached && outtakePipe.localPosition.y > intakePipe.localPosition.y + 0.1866)
        {
            tubesCorrectlyAttached = false;
            if (!triggered && dial.Value <= 0.2)
            {
                incorrectTrigger.Invoke();
                triggered = true;
            }
        }
    }

    // Fills the condenser with water when tubes are attached correctly and water is on
    private void HandleCondenserInternal()
    {        
        if (tubesCorrectlyAttached && dial.Value <= 0.2)
        {
            condenser.FillContainer("Water", material);
            PlayAudio(); // Plays bubbling SFX
        } else
        {
            condenser.EmptyContainer();
            PauseAudio(); // Pauses bubbling SFX
        }  
    }

    // Returns a bool value which indicates that the condenser is in its correct state for reflux success
    public bool IsReady()
    {
        return condenser.IsFull();
    }

    private void PlayAudio()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    private void PauseAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }
}
