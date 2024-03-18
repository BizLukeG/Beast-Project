using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ConditionDB 
{
    public static void Init()
    {
        foreach (var kvp in Conditions)
        {
            var conditionName = kvp.Key;
            var condition = kvp.Value;

            condition.Name = conditionName;
            //beast.FrontSprite = Resources.Load<Sprite>("Sprites/Brown");
        }
    }

    public static Dictionary<ConditionID, Condition> Conditions { get; set; } = new Dictionary<ConditionID, Condition>()
    {
        //beastBase to take values from 
        {
            ConditionID.Confused,
            new Condition(){
                ActivationMessage = "has been Confused",
                CurrentlyConfusedMessage = "is Confused",
                FullyConfusedMessage = "hurt itself in Confusion",
                Priority = 3,

                OnConditionActivated = (Beast beast) => {
                    //beast.Condition = ConditionID.Confused;
                    beast.ConditionCounter = UnityEngine.Random.Range(1,5);
                    //beast.ModifiedStats[StatID.Attack] = (int)Math.Round(.5 * beast.ModifiedStats[StatID.Attack], MidpointRounding.AwayFromZero);
                },
                OnBeforeMove = (Beast beast) =>
                {
                    Debug.Log("OnBeforeMove1 ");
                    int confusedNum;

                    if (beast.ConditionCounter == 0)
                    {
                        confusedNum = 0;
                        
                        return false;
                    }else
                    {
                        Debug.Log("OnBeforeMove2 ");
                        confusedNum = UnityEngine.Random.Range(1, 3); 
                        //Debug.Log("confusedNum1 " + confusedNum);
                        //Beast.BattleDialog.Enqueue($"{Beast.FoeString(beast)} {beast.Name} is {beast.Condition.ToString()}");
                        //beast.ConditionCounter--;
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
                OnRemoveCondition = (Beast beast) =>
                {
                    beast.BeastConditions.Remove(ConditionID.Confused);
                },
            }
        },
        {
            ConditionID.Flinched,
            new Condition()
            {
                FullyFlinchedMessage = "Flinched",
                //OnConditionActivated = (Beast beast) => {
                    
                //    beast.ConditionCounter = UnityEngine.Random.Range(1,5);
                    
                //},
                OnBeforeMove = (Beast beast) =>
                {
                    
                    int flinchNum = UnityEngine.Random.Range(1, 3);
                    if (flinchNum == 2)
                    {
                        return true;
                    }
                    return false;
                },
                OnRemoveCondition = (Beast beast) =>
                {
                    beast.BeastConditions.Remove(ConditionID.Flinched);
                },
            }
        }

    };
}
