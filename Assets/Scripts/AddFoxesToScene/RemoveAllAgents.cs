using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RemoveAllAgents : MonoBehaviour
{

    public static event EventHandler OnRemoveAllAgents;

    /// <summary>
    /// Sends an event which could be listened to what happens when the removeAllAgentButton is clicked
    /// </summary>
    public void RemoveAllAgentsButton()
    {
        OnRemoveAllAgents?.Invoke(this, EventArgs.Empty);
    }
}
