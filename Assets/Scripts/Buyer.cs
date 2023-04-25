using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Buyer : MonoBehaviour
{

    //buyers will buy something

    //The maximum price the buyer is willing to pay at all (suggestPrice can not go higher than this)
    [SerializeField] public float maxPrice = 60;

    //The price the buyer is willing to pay at this moment
    public float suggestPrice;


    public bool boughtItem = false;

    private Vector3 originalPosition;


    private List<Seller> stillNeedContact;
    private Seller currentSeller;

    private void Start()
    {
        suggestPrice = maxPrice;

        originalPosition = transform.position;
        DayManager.Instance.dayDone += DayManager_evaluateValue;
    }

    private void DayManager_evaluateValue(object sender, EventArgs e)
    {
        Reavaluate();
        currentSeller = null;
    }


    public void BuyItem()
    {
        boughtItem = true;

    }



    public bool StillHasBuyerToContact()
    {

        foreach(Seller seller in stillNeedContact)
        {
            if (seller.soldItem) continue;

            return true;
        }

        return false;
    }


    private IEnumerator WalkToPosition(Vector3 toPosition, float speed)
    {
        float timer = 0;

        Vector3 originalPosition = this.transform.position;


        //Change to do while with timer maybe
        while (timer < 1)
        {

            this.transform.position = Vector3.Lerp(originalPosition, toPosition, timer);
          
            yield return new WaitForSeconds(.01f / speed);
            timer += .01f * speed;
        } 
    }


    public void StartDay()
    {
        stillNeedContact = DayManager.Instance.GiveNewSellerList();
    }



    public void FindSeller()
    {
        Seller[] sellers = GameObject.FindObjectsOfType<Seller>();

        if (sellers.Length == 0) return;

        foreach (Seller seller in sellers)
        {
            if (!stillNeedContact.Contains(seller)) continue;

            if (seller.soldItem) continue;

            if (seller.occupied) continue;

            StartCoroutine(WalkToPosition(seller.stand.buyerPlaceTransform.position, DayManager.Instance.speed));

            currentSeller = seller;

            stillNeedContact.Remove(seller);
            

            seller.occupied = true;

            TryBuyFromSeller();

            break;
        }
    }


    public void TryBuyFromSeller()
    {
        currentSeller.TrySellProduct(suggestPrice, this);
    }

    public void WalkBack()
    {
        StartCoroutine(WalkToPosition(originalPosition, DayManager.Instance.speed));
        
    }

    


    public void Reavaluate()
    {
        if (boughtItem) suggestPrice--;
        else
        {
            suggestPrice++;
            if (suggestPrice > maxPrice) suggestPrice = maxPrice;
        }
        boughtItem = false;
    }


    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
