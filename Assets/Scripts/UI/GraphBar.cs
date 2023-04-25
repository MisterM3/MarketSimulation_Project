using UnityEngine;

public class GraphBar : MonoBehaviour
{

    //Graph has been put at 200 units max
    float maxHeight = 200.0f;
    //Size of bar at maxHeight
    float size = 5.0f;

    [Header("Current Price")]
    [SerializeField] float value = 60.0f;

    private Buyer buyer;
    private Seller seller;

    [Header("Prefabs")]
    [SerializeField] private GameObject priceObject;
    [SerializeField] private GameObject barObject;
    [SerializeField] private GameObject onlyBar;


    // Start is called before the first frame update
    void Start()
    {

        if (buyer != null)
        {
            MakeBuyerBar();
            return;
        }
        if (seller != null)
        {
            MakeSellerBar();
            return;
        }

        Debug.LogError("Bar has no seller or buyer! " + gameObject.name);

    }

    private void MakeBuyerBar()
    {
        value = buyer.maxPrice;
        SetBarScale();
        this.GetComponentInChildren<Renderer>().material.color = Color.red;
    }

    private void MakeSellerBar()
    {
        value = seller.minPrice;
        SetBarScale();
        this.GetComponentInChildren<Renderer>().material.color = Color.blue;

        if (seller.minPrice == seller.changedMinPrice) return;

        if (seller.minPrice > seller.changedMinPrice)
        {
            AddGovernmentChangeBar(new Vector3(0, 0, -.1f));
        }
        else
        {
            AddGovernmentChangeBar(new Vector3(0, 0, .1f));
        }
    }

    private void SetBarScale()
    {
        barObject.transform.localScale = new Vector3(1, (value / maxHeight) * size, .1f);
    }

    private void AddGovernmentChangeBar(Vector3 offset)
    {
        GameObject fullBar = Instantiate(onlyBar, this.transform.position + offset, Quaternion.identity, this.transform.parent);
        float value2 = seller.changedMinPrice;
        fullBar.transform.localScale = this.transform.localScale;
        fullBar.transform.GetChild(0).localScale = new Vector3(1, (value2 / maxHeight) * size, .1f);
        fullBar.GetComponentInChildren<Renderer>().material.color = Color.green;

        GetComponentInParent<MakeGraph>().AddBar(fullBar);
    }



    // Update is called once per frame
    void Update()
    {
        if (buyer != null)  SetPriceBarHeight(buyer.suggestPrice);
        
        if (seller != null) SetPriceBarHeight(seller.sellPrice);
    }

    public void SetSeller(Seller seller)
    {
       this.seller = seller;
    }

    public void SetBuyer(Buyer buyer)
    {
        this.buyer= buyer;
    }

    public void SetValue(float value)
    {
        this.value = value;
    }

    public void SetPriceBarHeight(float price)
    {
        priceObject.transform.localPosition = new Vector3(0.55f, (price / maxHeight) * size, -0.1f);
    }
}
