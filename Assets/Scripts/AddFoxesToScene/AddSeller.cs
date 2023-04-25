using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddSeller : MonoBehaviour
{


    public static event EventHandler<Seller> AddedSellerToScene;

    public float minPrice;


    [SerializeField] private TMP_InputField inputSingle;


    [SerializeField] GameObject sellerPrefab;


    public void SetMinPrice()
    {
        float inputNumber = float.Parse(inputSingle.text);

        if (inputNumber < 0)
        {
            inputNumber = 0;
            inputSingle.text = inputNumber.ToString();
        }
        if (inputNumber > 9999)
        {
            inputNumber = 9999;
            inputSingle.text = inputNumber.ToString();
        }

        minPrice = inputNumber;
    }

    public void SetMinPriceMultiple()
    {
        float inputNumber = float.Parse(inputSingle.text);

        if (inputNumber < 0)
        {
            inputNumber = 0;
            inputSingle.text = inputNumber.ToString();
        }
        if (inputNumber > 9999)
        {
            inputNumber = 9999;
            inputSingle.text = inputNumber.ToString();
        }

        minPrice = inputNumber;
    }



    public void AddSellerToScene()
    {
        SpawnSellerAndBuyer.Instance.AddSeller(minPrice);    
    }
}
