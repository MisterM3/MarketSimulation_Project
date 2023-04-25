using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GIManager : MonoBehaviour
{
    public static GIManager Instance { get; private set; }


    [SerializeField] GovernmentIntervention[] activeInterventions;

    [SerializeField] MinPrice minPrice;
    [SerializeField] MaxPrice maxPrice;

    public EventHandler onTaxesChanged;

    public void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Already Instance of GIManager in scene. Destroying: " + this.name);
            Destroy(this);
            return;
        }

        Instance = this;

        if (activeInterventions.Length != 0) return;
        activeInterventions = this.GetComponents<GovernmentIntervention>();
    }


    /// <summary>
    /// Adjust price to have taxes in them
    /// </summary>
    /// <param name="price"></param>
    /// <returns></returns>
    public float UseIntervention( float price)
    {

        if (activeInterventions.Length == 0) return price;

        foreach(GovernmentIntervention intervention in activeInterventions)
        {
            if (!intervention.isEnabled) continue;
            price = intervention.ChangePrice(price);
        }

        return price;
    }
    /// <summary>
    /// Changes price to adhere to minimum and maximum price
    /// </summary>
    /// <param name="price"></param>
    /// <returns></returns>
    public float CheckMinMax(float price)
    {
        if (maxPrice != null && maxPrice.isEnabled)
        {
            price = maxPrice.ChangePrice(price);
        }
        if (minPrice != null && minPrice.isEnabled)
        {
            price = minPrice.ChangePrice(price);
        }

        return price;
    }

    public void ChangedIntervention()
    {
        DayManager.Instance.ReadjustPrices();
        onTaxesChanged?.Invoke(this, EventArgs.Empty);
    }

    public float CalculateAmountEarned(float price)
    {
        if (activeInterventions.Length == 0) return 0;

        float amountBeforeTaxes = price;

        //Calculates amount before taxes, and gets the difference
        for (int i = activeInterventions.Length - 1; i >= 0; i--)
        {
            if (!activeInterventions[i].isEnabled) continue;
            amountBeforeTaxes = activeInterventions[i].RevertPrice(amountBeforeTaxes);
        }

        return price - amountBeforeTaxes;
    }

    public MinPrice GetMinPrice()
    {
        return minPrice;
    }

    public MaxPrice GetMaxPrice()
    {
        return maxPrice;
    }    
}
