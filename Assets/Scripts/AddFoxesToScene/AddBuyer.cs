using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddBuyer : MonoBehaviour
{


    [Header("Prices")]
    [SerializeField] float maxPrice;


    [Header("Inputs")]
    [SerializeField] private TMP_InputField input;
    [SerializeField] GameObject buyerPrefab;

    public static event EventHandler<Buyer> AddedBuyerToScene;


    /// <summary>
    /// Sets the inputField to be between 0-9999 and set the maxPrice for when a buyer is added
    /// </summary>
    public void SetMaxPrice()
    {
        float inputNumber = float.Parse(input.text);

        if (inputNumber < 0)
        {
            inputNumber = 0;
            input.text = inputNumber.ToString();
        }
        if (inputNumber > 9999)
        {
            inputNumber = 9999;
            input.text = inputNumber.ToString();
        }
        maxPrice = inputNumber;
    }

    /// <summary>
    /// Sends a event and ads a buyer to scene using SpawnSellerAndBuyer
    /// </summary>
    public void AddBuyerToScene()
    {
        SpawnSellerAndBuyer.Instance.AddBuyer(maxPrice);
    }
}
