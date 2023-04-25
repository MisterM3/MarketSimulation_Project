using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinPrice : GovernmentIntervention
{
    [SerializeField] float minimumPrice;

    public override float ChangePrice(float originalPrice)
    {
        return (originalPrice >= minimumPrice ? originalPrice : minimumPrice);
    }

    public override void ChangeAmounts(bool enabled, int amount)
    {
        isEnabled = enabled;
        minimumPrice = amount;
    }
    //Can't revert to original as start is not stored;
    public override float RevertPrice(float originalPrice)
    {
        return originalPrice;
    }

    public float GetMinPrice()
    {
        return minimumPrice;
    }
}
