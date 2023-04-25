using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddMultipleSellers : MonoBehaviour
{
    public float minPrice;

    public float maxPrice;

    public int amount;
    [SerializeField] private TMP_InputField inputMultipleMin;
    [SerializeField] private TMP_InputField inputMultipleMax;
    [SerializeField] private TMP_InputField inputAmount;



    public void SetMinPriceMultiple()
    {
        float inputNumber = float.Parse(inputMultipleMin.text);

        if (inputNumber < 0)
        {
            inputNumber = 0;
            inputMultipleMin.text = inputNumber.ToString();
        }
        if (inputNumber > 9999)
        {
            inputNumber = 9999;
            inputMultipleMin.text = inputNumber.ToString();
        }

        minPrice = inputNumber;
    }

    public void SetMaxPriceMultiple()
    {
        float inputNumber = float.Parse(inputMultipleMax.text);

        if (inputNumber < 0)
        {
            inputNumber = 0;
            inputMultipleMax.text = inputNumber.ToString();
        }
        if (inputNumber > 9999)
        {
            inputNumber = 9999;
            inputMultipleMax.text = inputNumber.ToString();
        }

        maxPrice = inputNumber;
    }

    public void SetAmountMultiple()
    {

        float inputNumber = float.Parse(inputAmount.text);

        if (inputNumber < 0)
        {
            inputNumber = 0;
            inputAmount.text = inputNumber.ToString();
        }
        if (inputNumber > 50)
        {
            inputNumber = 50;
            inputAmount.text = inputNumber.ToString();
        }

        amount = (int)inputNumber;
    }

    public void AddMultipleSellersToScene()
    {

        float pricePerMan;

        if (maxPrice > minPrice) pricePerMan = (maxPrice - minPrice) / amount;
        else pricePerMan = (minPrice - maxPrice) / amount;


        float minimum = Mathf.Min(minPrice, maxPrice);

        for (int i = 0; i < amount; i++)
        {
            SpawnSellerAndBuyer.Instance.AddSeller(minimum + pricePerMan * i);

        }
    }

    public void AddMultipleBuyersToScene()
    {

        float pricePerMan;

        if (maxPrice > minPrice) pricePerMan = (maxPrice - minPrice) / amount;
        else pricePerMan = (minPrice - maxPrice) / amount;


        float minimum = Mathf.Min(minPrice, maxPrice);

        for (int i = 0; i < amount; i++)
        {
            SpawnSellerAndBuyer.Instance.AddBuyer(minimum + pricePerMan * i);
        }
    }

}
