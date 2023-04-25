using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class InformationUI : MonoBehaviour
{


    [SerializeField] TextMeshProUGUI amountSoldText;

    [SerializeField] TextMeshProUGUI minPriceText;
    [SerializeField] TextMeshProUGUI maxPriceText;
    [SerializeField] TextMeshProUGUI mediumPriceText;
    [SerializeField] TextMeshProUGUI governmentMoneyText;


    private float minPrice;

    private float maxPrice;

    private int amountSold;

    private float totalAmountMoneyTraded;

    private float governmentMoney;

    // Start is called before the first frame update
    void Start()
    {
        Seller.onAnyItemSold += Seller_onAnyItemSold;
        DayManager.Instance.dayDone += DayManager_dayDone;
        GIManager.Instance.onTaxesChanged += ResetEvent;

        SpawnSellerAndBuyer.AddedBuyerToScene += SpawnSellerAndBuyer_AddedBuyerToScene;
        SpawnSellerAndBuyer.AddedSellerToScene += SpawnSellerAndBuyer_AddedSellerToScene;
        Reset();
    }

    private void SpawnSellerAndBuyer_AddedSellerToScene(object sender, Seller e)
    {
        ResetTexts();
    }

    private void SpawnSellerAndBuyer_AddedBuyerToScene(object sender, Buyer e)
    {
        ResetTexts();
    }


    private void ResetEvent(object sender, EventArgs e)
    {
        ResetTexts();
    }

    private void DayManager_dayDone(object sender, EventArgs e)
    {
        Reset();
    }

    private void Seller_onAnyItemSold(object sender, float moneySold)
    {
        UpdateAmountSold();
        UpdateMinPrice(moneySold);
        UpdateMediumPrice(moneySold);
        UpdateMaxPrice(moneySold);
        UpdateGovernmentMoney(moneySold);
    }




    public void UpdateMinPrice(float priceCheck)
    {

        Debug.Log(priceCheck);
        if (priceCheck > minPrice) return;

        minPrice = priceCheck;

        minPriceText.text = $"Minimum: {minPrice}"; 

    }

    public void UpdateMediumPrice(float priceCheck)
    {
        totalAmountMoneyTraded += priceCheck;

        float mediumPrice = totalAmountMoneyTraded / amountSold;

        mediumPriceText.text = $"Medium: {mediumPrice}";
    }

    public void UpdateMaxPrice(float priceCheck)
    {
        if (priceCheck < maxPrice) return;

        maxPrice = priceCheck;

        maxPriceText.text = $"Maximum: {maxPrice}";

    }

    public void UpdateAmountSold()
    {
        amountSold++;

        amountSoldText.text = $"Amount Sold: {amountSold}";

    }

    

    public void UpdateGovernmentMoney(float priceSold)
    {
        governmentMoney += GIManager.Instance.CalculateAmountEarned(priceSold);



        governmentMoneyText.text = $"Government Earned: {governmentMoney.ToString("F2")}";
    }

    public void Reset()
    {
        amountSold = 0;
        minPrice = float.MaxValue;
        maxPrice = 0;
        governmentMoney = 0;
        totalAmountMoneyTraded = 0;

    }

    public void ResetTexts()
    {
        minPriceText.text = $"Minimum: 0";
        mediumPriceText.text = $"Medium: 0";
        maxPriceText.text = $"Maximum: 0";
        amountSoldText.text = $"Amount Sold: 0";
        governmentMoneyText.text = $"Government Earned: 0";
    }
}
