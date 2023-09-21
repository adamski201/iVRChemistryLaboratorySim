using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// An interactable knob that follows the rotation of the interactor
/// </summary>
public class DialInteractable : XRBaseInteractable
{
    [Tooltip("The transform of the visual component of the knob")]
    public Transform knobTransform = null;

    [Tooltip("The angle at which the dial's value is 0")]
    [Range(-180, 180)] public float angleMinimum = -90.0f;

    [Tooltip("The angle at which the dial's value is 1")]
    [Range(-180, 180)] public float angleMaximum = 90.0f;

    [Tooltip("The initial value of the knob (0..1)")]
    [Range(0, 1)] public float defaultValue = 0.0f;

    [Tooltip("Whether turning clockwise increases the value or not")]
    public bool clockwiseIsPositive = true;
    [Serializable] public class ValueChangeEvent : UnityEvent<float> { }

    // When the knobs's value changes
    public ValueChangeEvent OnValueChange = new ValueChangeEvent();

    public float Value { get; private set; } = 0.0f;
    public float Angle { get; private set; } = 0.0f;

    private IXRSelectInteractor selectInteractor = null;
    private Quaternion selectRotation = Quaternion.identity;

    private void Start()
    {
        float defaultRotation;
        if (clockwiseIsPositive)
            defaultRotation = Mathf.Lerp(angleMaximum, angleMinimum, defaultValue);
        else
            defaultRotation = Mathf.Lerp(angleMinimum, angleMaximum, defaultValue);
        SetRotation(defaultRotation);
        SetValue(defaultRotation);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        selectEntered.AddListener(StartTurn);
        selectExited.AddListener(EndTurn);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        selectEntered.RemoveListener(StartTurn);
        selectExited.RemoveListener(EndTurn);
    }

    private void StartTurn(SelectEnterEventArgs eventArgs)
    {
        selectInteractor = eventArgs.interactorObject;
        selectRotation = selectInteractor.transform.rotation;
    }

    private void EndTurn(SelectExitEventArgs eventArgs)
    {
        selectInteractor = null;
        selectRotation = Quaternion.identity;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (isSelected)
            {
                Angle = FindRotationValue();
                float finalRotation = ApplyRotation(Angle);

                SetValue(finalRotation);
                selectRotation = selectInteractor.transform.rotation;
            }
        }
    }

    private float FindRotationValue()
    {
        Quaternion rotationDifference = selectInteractor.transform.rotation * Quaternion.Inverse(selectRotation);
        Vector3 rotatedForward = rotationDifference * knobTransform.forward;
        return (Vector3.SignedAngle(knobTransform.forward, rotatedForward, transform.up));
    }

    private float SetRotation(float angle)
    {
        Quaternion newRotation = Quaternion.AngleAxis(angle, Vector3.up);

        Vector3 eulerRotation = newRotation.eulerAngles;
        float unclamped = eulerRotation.y;
        eulerRotation.y = ClampAngle(eulerRotation.y);
        knobTransform.localEulerAngles = eulerRotation;

        return eulerRotation.y;
    }
    private float ApplyRotation(float angle)
    {
        Quaternion newRotation = Quaternion.AngleAxis(angle, Vector3.up);
        newRotation *= knobTransform.localRotation;

        Vector3 eulerRotation = newRotation.eulerAngles;
        eulerRotation.y = ClampAngle(eulerRotation.y);
        knobTransform.localEulerAngles = eulerRotation;
        return eulerRotation.y;
    }

    private float ClampAngle(float angle)
    {
        if (angle > 180)
            angle -= 360;

        return Mathf.Clamp(angle, angleMinimum, angleMaximum);
    }

    private void SetValue(float rotation)
    {
        Value = Mathf.InverseLerp(angleMinimum, angleMaximum, rotation);
        if (clockwiseIsPositive) Value = 1.0f - Value;
        OnValueChange.Invoke(Value);
    }
}

