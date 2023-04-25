using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mono.Cecil;

public class BuyerUI : MonoBehaviour
{

    [SerializeField] Buyer buyer;
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
        if (buyer == null) return;

        costText.text = $"Cost: {buyer.suggestPrice} \n Max:{buyer.maxPrice}"; 
    }

    public void VisualUpdate()
    {
        if (buyer == null) return;

        if (buyer.boughtItem) visualPrefabRenderer.material.color = Color.green;
        else visualPrefabRenderer.material.color = Color.red;
    }
}
