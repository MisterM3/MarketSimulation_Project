using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{


    public event EventHandler dayDone;
    public event EventHandler sellTime;


    public float speed = 10f;

    List<Buyer> buyerList;
    List<Seller> sellerList;
    List<Buyer> needToStilTryBuy;

    private IEnumerator coroutine;

    public static DayManager Instance { get; private set; }

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        coroutine = Day();


            sellerList = new List<Seller>(GameObject.FindObjectsOfType<Seller>());
            buyerList = new List<Buyer>(GameObject.FindObjectsOfType<Buyer>());
            needToStilTryBuy = new List<Buyer>();
             StartCoroutine(coroutine);

            RemoveAllAgents.OnRemoveAllAgents += RemoveAllAgents_OnRemoveAllAgents;

            return;
        }

        Destroy(this.gameObject);
        Debug.Log("Already a DayManager in the scene deleting " + this.gameObject);

    }

    private void RemoveAllAgents_OnRemoveAllAgents(object sender, EventArgs e)
    {
        RemoveAll();
    }

    private void RemoveAll()
    {
        foreach (Buyer buyer in buyerList)
        {
            Destroy(buyer.gameObject);
        }

        foreach (Seller seller in sellerList)
        {
            Destroy(seller.stand.gameObject);
        }

        buyerList.Clear();
        sellerList.Clear();
        needToStilTryBuy.Clear();
    }



    public void ReadjustPrices()
    {
        GetAllBuyersAndSellers();

        foreach(Seller seller in sellerList)
        {
            seller.ReadjustPriceInterventions();
        }
    }
    
    /// <summary>
    /// Runs day cycle indefenitly
    /// </summary>
    /// <returns></returns>
    private IEnumerator Day()
    {



        while (true)
        {
            GetAllBuyersAndSellers();

            if (buyerList.Count == 0 || sellerList.Count == 0)
            {
                yield return new WaitForSeconds(1);
                continue;
            }
            RandomizeBuyers();
            StartDayTimer();

            while (CheckIfNeedBuy())
            {
                yield return new WaitForSeconds(0.1f / speed);
                FindTime();
                sellTime?.Invoke(this, EventArgs.Empty);
                yield return new WaitForSeconds(2f / speed);
                WalkBack();
            }
            yield return new WaitForSeconds(2f / speed);
            dayDone?.Invoke(this, EventArgs.Empty);
        }
    }


    private void GetAllBuyersAndSellers()
    {
        sellerList = new List<Seller>(GameObject.FindObjectsOfType<Seller>());
        buyerList = new List<Buyer>(GameObject.FindObjectsOfType<Buyer>());
    }

    private void StartDayTimer()
    {

        foreach (Buyer buyer in buyerList)
        {
            buyer.StartDay();
        }
    }

    private bool CheckIfNeedBuy()
    {

        needToStilTryBuy.Clear();

        foreach (Buyer buyer in buyerList)
        {
            if (buyer.boughtItem) continue;

            if (!buyer.StillHasBuyerToContact()) continue;

            needToStilTryBuy.Add(buyer);

            return true;
        }

        return false;

    }

    private void FindTime()
    {

        foreach (Buyer buyer in needToStilTryBuy)
        {
            buyer.FindSeller();
        }

    }

    private void WalkBack()
    {
        foreach (Buyer buyer in needToStilTryBuy)
        {
            buyer.WalkBack();
        }

        foreach(Seller seller in sellerList)
        {
            seller.occupied = false;
        }
    }


    private void RandomizeBuyers()
    {


        for (int i = 0; i < buyerList.Count; i++)
        {
            Buyer temp = buyerList[i];
            int randomIndex = UnityEngine.Random.Range(i, buyerList.Count);
            buyerList[i] = buyerList[randomIndex];
            buyerList[randomIndex] = temp;
        }

        if (needToStilTryBuy.Count <= 0) return;

        for (int i = 0; i < needToStilTryBuy.Count; i++)
        {
            Buyer temp = needToStilTryBuy[i];
            int randomIndex = UnityEngine.Random.Range(i, needToStilTryBuy.Count);
            needToStilTryBuy[i] = needToStilTryBuy[randomIndex];
            needToStilTryBuy[randomIndex] = temp;
        }


    }

    public List<Seller> GiveNewSellerList()
    {
        return new List<Seller>(sellerList);
    }
}
