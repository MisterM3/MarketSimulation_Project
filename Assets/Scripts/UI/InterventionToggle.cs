using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;

[Serializable]
public class OnInterventionToggleChangedEvent : UnityEvent<bool, int>
{

}

public class InterventionToggle : MonoBehaviour
{
    private bool govEnabled = false;

    private int amount = 0;

    [SerializeField] private TMP_InputField input;

    public OnInterventionToggleChangedEvent onInterventionChanged;


    public void OnValueChanged()
    {
        onInterventionChanged?.Invoke(govEnabled, amount);
    }



    public void ChangeEnabled(bool enabled)
    {
        govEnabled = enabled;
        OnValueChanged();
    }

    public void SetAmount()
    {
        if (input == null)
        {
            Debug.LogError("No Input at" + gameObject.name);
            return;
        }

        int inputNumber = int.Parse(input.text);
        amount = inputNumber;
        OnValueChanged();
    }

}
