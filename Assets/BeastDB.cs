using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BeastID
{
    Expurn, pugba, lustorm
}

public class BeastDB : MonoBehaviour
{
    public static Dictionary<BeastID, Beast> Beasts { get; set; } = new Dictionary<BeastID, Beast>()
    {
        
        {BeastID.Expurn,
            new Expurn()
        }
    };
}



