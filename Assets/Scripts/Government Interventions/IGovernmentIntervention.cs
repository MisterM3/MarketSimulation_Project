using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GovernmentIntervention : MonoBehaviour
{

    public bool isEnabled = false;


    public abstract float ChangePrice(float originalPrice);

    public abstract float RevertPrice(float originalPrice);


    //Changed if the intervention is active, and the value of the intervention
    public abstract void ChangeAmounts(bool enabled, int amount);
}
