using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketStand : MonoBehaviour
{

    [SerializeField] public Transform buyerPlaceTransform;

    [SerializeField] public Seller seller;

    [SerializeField] GameObject product;

    public void Start()
    {
        seller.changedIfHasProduct += Seller_changedIfHasProduct;
    }

    private void Seller_changedIfHasProduct(object sender, bool hasProduct)
    {
        product.SetActive(hasProduct);
    }

    private void OnDestroy()
    {
        seller.changedIfHasProduct -= Seller_changedIfHasProduct;
    }
}
