using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeGraph : MonoBehaviour
{

    [SerializeField] float widthGraph = 5.5f;
    [SerializeField] float heightGraph = 5.5f;

    [SerializeField] List<Seller> sellers;
    [SerializeField] List<Buyer> buyers;
    [SerializeField] GameObject fullBarPrefab;

    [SerializeField] List<GameObject> bars;

    [SerializeField] Toggle toggle;

    [Header("Min,MaxPrice")]
    [SerializeField] GameObject minMaxPriceBar;

    [SerializeField] bool overlap = false;
    // Start is called before the first frame update
    void Start()
    {

        SpawnSellerAndBuyer.AddedBuyerToScene += SpawnSellerAndBuyer_AddedBuyerToScene;
        SpawnSellerAndBuyer.AddedSellerToScene += SpawnSellerAndBuyer_AddedSellerToScene;

        RemoveAllAgents.OnRemoveAllAgents += RemoveAllAgents_OnRemoveAllAgents;
        GIManager.Instance.onTaxesChanged += GIManager_TaxesChanged;
        MakeGraphVisual();
    }

    private void SpawnSellerAndBuyer_AddedSellerToScene(object sender, Seller e)
    {
        sellers.Add(e);
        ResetGraph();
    }

    private void SpawnSellerAndBuyer_AddedBuyerToScene(object sender, Buyer e)
    {
        buyers.Add(e);
        ResetGraph();
    }


    private void GIManager_TaxesChanged(object sender, System.EventArgs e)
    {
        ResetGraph();
    }


    private void RemoveAllAgents_OnRemoveAllAgents(object sender, System.EventArgs e)
    {
        sellers.Clear();
        buyers.Clear();
        RemoveGraph();
    }

    public void ChangeOverlay()
    {
        overlap = toggle.isOn;

        ResetGraph();
    }
    void MakeGraphVisual()
    {
        int amountOfBuyersAndSellers = sellers.Count + buyers.Count;

        if (amountOfBuyersAndSellers == 0) return;

        float perAmount = CalculateSizePerBar(amountOfBuyersAndSellers);


        int amountOfBars = 0;

        SortLists();


        foreach (Seller seller in sellers)
        {
            MakeBar(perAmount, amountOfBars).SetSeller(seller);
            amountOfBars++;
        }

        foreach (Buyer buyer in buyers)
        {
            MakeBar(perAmount, amountOfBars).SetBuyer(buyer);
            amountOfBars++;
        }
    }

    void MakeGraphVisualOverlap()
    {
        int maxAmountSellersOrBuyers = (sellers.Count > buyers.Count ? sellers.Count : buyers.Count);

        if (maxAmountSellersOrBuyers == 0) return;

        float perAmount = CalculateSizePerBar(maxAmountSellersOrBuyers);

        int amountOfBars = 0;

        SortLists();


        foreach (Seller seller in sellers)
        {
            MakeBar(perAmount, amountOfBars).SetSeller(seller);
            amountOfBars++;
        }

        //Reset as start overlay
        amountOfBars = 0;

        foreach (Buyer buyer in buyers)
        {
            MakeBar(perAmount, amountOfBars).SetBuyer(buyer);
            amountOfBars++;
        }
    }


    private GraphBar MakeBar(float perAmount, float barNumber)
    {
        GameObject fullBar = Instantiate(fullBarPrefab, this.transform);
        fullBar.transform.localPosition = new Vector3(perAmount + 3f * perAmount * barNumber, -2.72f, 0);
        fullBar.transform.localScale = new Vector3(perAmount * 2.0f, 1, 1);

        bars.Add(fullBar);
        return fullBar.GetComponent<GraphBar>();
    }

    private float CalculateSizePerBar(float amountSplit)
    {
        //bars are twice the width than the whitespace;
        float divideGraph = (amountSplit + 1) + (amountSplit * 2);

        float perAmount = widthGraph / divideGraph;
        return perAmount;
    }

    private void SortLists()
    {
        sellers.Sort((x, y) => x.minPrice.CompareTo(y.minPrice));
        buyers.Sort((x, y) => y.maxPrice.CompareTo(x.maxPrice));
    }



    void ResetGraph()
    {
        RemoveGraph();


        if (overlap) MakeGraphVisualOverlap();
        else MakeGraphVisual();
    }

    void RemoveGraph()
    {
        foreach (GameObject bar in bars)
        {
            Destroy(bar);
        }

        bars.Clear();
    }

    public void AddBar(GameObject bar)
    {
        bars.Add(bar);
    }
}
