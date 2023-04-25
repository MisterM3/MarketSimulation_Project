using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxPrice : GovernmentIntervention
{
    [SerializeField] float maximumPrice;

    public override float ChangePrice(float originalPrice)
    {
        return (originalPrice <= maximumPrice ? originalPrice : maximumPrice);
    }

    public override void ChangeAmounts(bool enabled, int amount)
    {
        isEnabled = enabled;
        maximumPrice = amount;
    }


    //Can't revert price
    public override float RevertPrice(float originalPrice)
    {
        return originalPrice;
    }


    public float GetMaxPrice()
    {
        return maximumPrice;
    }

}
