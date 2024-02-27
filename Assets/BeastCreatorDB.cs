using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum BeastID
{
    expurn, pugba, lustorm
}

public class BeastCreatorDB : MonoBehaviour
{
    public static Dictionary<BeastID, Func<int, Beast>> BeastCreators { get; set; } = new Dictionary<BeastID, Func<int, Beast>>()
    {
        //new Beast() with subSpecies as a property 
        {BeastID.expurn, Expurn.CreateNewExpurn},
        {BeastID.pugba, Pugba.CreateNewPugba},
        {BeastID.lustorm, Lustrom.CreateNewLustrom}
    };
}

//dude does the object instantiation syntax because that is the only way to pass in info into the dictionary

