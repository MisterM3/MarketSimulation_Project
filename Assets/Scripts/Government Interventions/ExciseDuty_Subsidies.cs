using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExciseDuty_Subsidies : GovernmentIntervention
{

    /// Positive is Excise Duty, Negative is Subsidies
    public float floatAmount;


    public override float ChangePrice(float originalPrice)
    {
        return originalPrice += floatAmount;
    }

    public override void ChangeAmounts(bool enabled, int amount)
    {
        isEnabled = enabled;
        floatAmount = amount;
    }

    public override float RevertPrice(float originalPrice)
    {
        return originalPrice -= floatAmount;
    }
}
