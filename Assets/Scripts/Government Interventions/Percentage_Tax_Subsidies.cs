using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Percentage_Tax_Subsidies : GovernmentIntervention
{

    /// Positive is Taxes, Negative is Subsidies
    [Range(-100,100)]
    public int percentageAmount;


    public override float ChangePrice(float originalPrice)
    {

       float percentage = percentageAmount * 0.01f;
       return originalPrice += originalPrice * percentage;
    }

    public override void ChangeAmounts(bool enabled, int amount)
    {
        Debug.Log("test");
        isEnabled = enabled;
        percentageAmount = amount;
    }

    public override float RevertPrice(float originalPrice)
    {
        float percentage = percentageAmount * 0.01f;
        return originalPrice / (percentage + 1);
    }


}
