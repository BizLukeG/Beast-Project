using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum BeastID
{
    Expurn, Pugba, Lustorm
}

public class BeastBaseDB : MonoBehaviour
{

    public static Dictionary<BeastID, Beast> BeastBases { get; set; } = new Dictionary<BeastID, Beast>()
    {
        //beastBase to take values from 
        {   
            BeastID.Expurn, 
            new Beast(){
                //Name = "Expurn",
                MaxBaseStats = 300,
                Typing1 = Typing.Static,
                Typing2 = Typing.Corrupt,
                LearnSet = new Dictionary<int, MoveID>()
                {
                    { 1, MoveID.Smack }, {3, MoveID.Pound}, {4, MoveID.Tackle}, {5, MoveID.LeafStorm},  {6, MoveID.FlameWheel}, {7, MoveID.RockSlide}

                }
            }
        },
        {
            BeastID.Pugba,
            new Beast(){
                //Name = "Pugba",
                MaxBaseStats = 200,
                Typing1 = Typing.Native,
                Typing2 = Typing.None,
                LearnSet = new Dictionary<int, MoveID>()
                {
                    { 1, MoveID.Smack }, {3, MoveID.Pound}, {4, MoveID.Tackle}, {5, MoveID.LeafStorm},  {6, MoveID.FlameWheel}, {7, MoveID.RockSlide}

                }
            }
        },
        {
            BeastID.Lustorm,
            new Beast(){
                //Name = "Lustorm",
                MaxBaseStats = 400,
                Typing1 = Typing.Aerial,
                Typing2 = Typing.None,
                LearnSet = new Dictionary<int, MoveID>()
                {
                    { 1, MoveID.Smack }, {3, MoveID.Pound}, {4, MoveID.Tackle}, {5, MoveID.LeafStorm},  {6, MoveID.FlameWheel}, {7, MoveID.RockSlide}

                }
            }
        },
        
    };

    public static void Initialize()
    {
        foreach (var kvp in BeastBases)
        {
            var beastName = kvp.Key;
            var beast = kvp.Value;

            beast.Name = beastName;
        }
    }
}

//dude does the object instantiation syntax because that is the only way to pass in info into the dictionary
