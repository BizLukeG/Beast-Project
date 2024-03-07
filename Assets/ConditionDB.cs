using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ConditionDB 
{
    public static Dictionary<ConditionID, Condition> Conditions { get; set; } = new Dictionary<ConditionID, Condition>()
    {
        //beastBase to take values from 
        {
            ConditionID.Confused,
            new Condition(){
                ActivationMessage = "has been confused.",

                OnConditionActivated = (Beast beast) => {
                    //beast.Condition = ConditionID.Confused;
                    beast.ConditionCounter = UnityEngine.Random.Range(1,5);
                    //beast.ModifiedStats[StatID.Attack] = (int)Math.Round(.5 * beast.ModifiedStats[StatID.Attack], MidpointRounding.AwayFromZero);
                },
            }
        },


    };
}
