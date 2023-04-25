using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seller : MonoBehaviour
{
    //Seller will sell something


    [SerializeField] public MarketStand stand;

    //The maximum price the seller is willing to sell at all (sellPrice can not go lower than this)
    [SerializeField] public float minPrice = 20;
    public float changedMinPrice;

    //The price the seller is willing to sell it's product at this moment
    public float sellPrice;

    public bool occupied = false;

    public bool soldItem = false;



    //Done all the things they could in a day (e.g sold item or checked with everyone available)
    public bool doneDay = false;

    public static EventHandler<float> onAnyItemSold;

    public EventHandler<bool> changedIfHasProduct;

    


    public void Start()
    {

        changedMinPrice = GIManager.Instance.UseIntervention(minPrice);
        sellPrice = changedMinPrice;

        DayManager.Instance.dayDone += DayManager_evaluateValue;
    }


    public void ReadjustPriceInterventions()
    {
        changedMinPrice = GIManager.Instance.UseIntervention(minPrice);

        if (sellPrice < changedMinPrice) sellPrice = changedMinPrice;
    }

    private void DayManager_evaluateValue(object sender, EventArgs e)
    {
        Reavaluate();
    }

    public bool TrySellProduct(float buyPrice, Buyer buyer)
    {
        if (buyPrice >= sellPrice)
        {
            SellProduct(buyer);
            return true;
        }

        return false;
    }



    public void SellProduct(Buyer buyer)
    {
        buyer.BuyItem();
        soldItem = true;
        onAnyItemSold?.Invoke(this, sellPrice);
        changedIfHasProduct?.Invoke(this, false);
    }

    public void Reavaluate()
    {

        if (soldItem)
        {
            sellPrice++;
            changedIfHasProduct?.Invoke(this, true);
        }
        else
        {
            sellPrice--;

            if (sellPrice < changedMinPrice) sellPrice = changedMinPrice;

        }

        sellPrice = GIManager.Instance.CheckMinMax(sellPrice);
        

        soldItem = false;
        occupied = false;
        if (sellPrice < changedMinPrice)
        {
            sellPrice = changedMinPrice;
            soldItem = true;
            changedIfHasProduct?.Invoke(this, false);
        }
    }
}
