using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum BeastID
{
    expurn, pugba, lustorm
}

public class BeastBaseDB : MonoBehaviour
{
    public static Dictionary<BeastID, Beast> BeastBases { get; set; } = new Dictionary<BeastID, Beast>()
    {
        //beastBase to take values from 
        {   
            BeastID.expurn, 
            new Beast(){
                Name = "Expurn",
                MaxBaseStats = 300,
            }
        },
        {
            BeastID.pugba,
            new Beast(){
                Name = "Pugba",
                MaxBaseStats = 200,
            }
        },
        {
            BeastID.lustorm,
            new Beast(){
                Name = "Lustorm",
                MaxBaseStats = 400,
            }
        },
        
    };
}

//dude does the object instantiation syntax because that is the only way to pass in info into the dictionary

