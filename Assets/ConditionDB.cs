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
                FullyConfusedMessage = "hurt itself in confusion.",

                OnConditionActivated = (Beast beast) => {
                    //beast.Condition = ConditionID.Confused;
                    beast.ConditionCounter = UnityEngine.Random.Range(1,5);
                    //beast.ModifiedStats[StatID.Attack] = (int)Math.Round(.5 * beast.ModifiedStats[StatID.Attack], MidpointRounding.AwayFromZero);
                },
                OnBeforeMove = (Beast beast) =>
                {
                    int confusedNum;

                    if (beast.ConditionCounter == 0)
                    {
                        confusedNum = 0;
                        Beast.BattleDialog.Enqueue($"{Beast.FoeString(beast)} {beast.Name} snapped out of confusion");
                        beast.Condition = ConditionID.None;
                        return false;
                    }else 
                    {
                        confusedNum = UnityEngine.Random.Range(1, 3); /*2*/;
                        Debug.Log("confusedNum1 " + confusedNum);
                        Beast.BattleDialog.Enqueue($"{Beast.FoeString(beast)} {beast.Name} is {beast.Condition.ToString()}");
                        beast.ConditionCounter--;
                        if(confusedNum == 2)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                },
            }
        },


    };
}
