using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Min_MaxPriceBar : MonoBehaviour
{

    [SerializeField] GameObject barPrefab;

    //Graph has been put at 200 units max
    float maxHeight = 200.0f;
    //Size of bar at maxHeight
    float size = 5.0f;

    GameObject minBar;
    GameObject maxBar;

    

    // Start is called before the first frame update
    void Start()
    {

        GIManager.Instance.onTaxesChanged += GIManager_OnTaxesChanged;

        AddBars();
        DeacitvateBarMax();
        DeacitvateBarMin();
    }

    private void GIManager_OnTaxesChanged(object sender, EventArgs e)
    {
        MinPrice min = GIManager.Instance.GetMinPrice();
        MaxPrice max = GIManager.Instance.GetMaxPrice();

        if (min.isEnabled) ActivateBarMin(min.GetMinPrice());
        else DeacitvateBarMin();
        
        if (max.isEnabled) ActivateBarMax(max.GetMaxPrice());
        else DeacitvateBarMax();
        Debug.Log(min.isEnabled);
        Debug.Log(max.isEnabled);
    }

    private void ActivateBarMin(float price)
    {
        Debug.Log((price / maxHeight) * size);
        minBar.transform.localPosition = new Vector3(0.55f, (price / maxHeight) * size, -0.1f);
        minBar.SetActive(true);
    }

    private void ActivateBarMax(float price)
    {
        maxBar.transform.localPosition = new Vector3(0.55f, (price / maxHeight) * size, -0.1f);
        maxBar.SetActive(true);
    }

    private void AddBars()
    {
        minBar = Instantiate(barPrefab, this.transform);
        maxBar = Instantiate(barPrefab, this.transform);
    }

    private void DeacitvateBarMin()
    {
        minBar.SetActive(false);
    }

    private void DeacitvateBarMax()
    {
        maxBar.SetActive(false);
    }

    private void OnDestroy()
    {
        GIManager.Instance.onTaxesChanged -= GIManager_OnTaxesChanged;
    }

}
