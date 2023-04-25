using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SellerUI : MonoBehaviour
{
    [SerializeField] Seller seller;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] Renderer visualPrefabRenderer;



    // Update is called once per frame
    void Update()
    {
        TextUpdate();
        VisualUpdate();
    }


    public void TextUpdate()
    {
        if (seller == null) return;

        costText.text = $"Cost: {seller.sellPrice} \n Min:{seller.minPrice}";
    }

    public void VisualUpdate()
    {
        if (seller == null) return;

        if (seller.soldItem) visualPrefabRenderer.material.color = Color.green;
        else visualPrefabRenderer.material.color = Color.red;
    }
}
