using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnSellerAndBuyer : MonoBehaviour
{
    [Header ("Parent object")]
    [SerializeField] Transform sellerParent;
    [SerializeField] Transform buyerParent;

    [Header("Prefabs")]
    [SerializeField] GameObject sellerPrefab;
    [SerializeField] GameObject buyerPrefab;

    [Header("SpawnRadius")]
    [SerializeField] float angle;
    [SerializeField] float distance;


    [Header("Offset")]
    [SerializeField] float angleOffset;

    public static SpawnSellerAndBuyer Instance { get; private set; }

    public static event EventHandler<Seller> AddedSellerToScene;
    public static event EventHandler<Buyer> AddedBuyerToScene;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Already a SpawnSellerAndBuyer in scene, deleting: " + gameObject.name);
            Destroy(this);
            return;
        }


        Instance = this;
    }

    /// <summary>
    /// Adds seller to scene with a minimum Price of minPrice
    /// </summary>
    /// <param name="minPrice">Minimum Price of Seller</param>
    /// <returns> Added Seller</returns>
    public Seller AddSeller(float minPrice)
    {
        Vector3 pos = RandomPoint();
        GameObject ob = Instantiate(sellerPrefab, pos, Quaternion.Euler(0, 90, 0), sellerParent);
        Seller newSeller = ob.GetComponent<MarketStand>().seller;
        newSeller.minPrice = minPrice;
        AddedSellerToScene?.Invoke(this, newSeller);
        return newSeller;
    }

    /// <summary>
    /// Adds buyer to scene with a maximum price of maxPrice
    /// </summary>
    /// <param name="minPrice">Minimum Price of Buyer</param>
    /// <returns> Added Seller</returns>
    public Buyer AddBuyer(float maxPrice)
    {
        Vector3 pos = RandomPoint();
        GameObject ob = Instantiate(buyerPrefab, pos, Quaternion.Euler(0, 90, 0), buyerParent);
        Buyer newBuyer = ob.GetComponent<Buyer>();
        newBuyer.maxPrice = maxPrice;
        AddedBuyerToScene?.Invoke(this, newBuyer);
        return newBuyer;
    }

    /// <summary>
    /// Calculates random point where the agents will spawn
    /// </summary>
    /// <returns> Random Point inbetween angle and range</returns>
    private Vector3 RandomPoint()
    {
        Vector3 pos;

        float randomAngle = UnityEngine.Random.Range(angleOffset, angle + angleOffset);
        float randomDistance = UnityEngine.Random.Range(0, distance);
        
        pos.x = randomDistance * Mathf.Sin(randomAngle * Mathf.Deg2Rad);
        pos.y = 0;
        pos.z = randomDistance * Mathf.Cos(randomAngle * Mathf.Deg2Rad);

        return pos;
    }
}
