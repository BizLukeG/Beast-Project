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
        //Beast have multiple conditions and can have conditions and statuses, but can't have multiple statuses at the same time
        
        
        {
            ConditionID.Confused,
            new Condition(){
                ActivationMessage = "has been Confused",
                CurrentlyConfusedMessage = "is Confused",
                FullyConfusedMessage = "hurt itself in Confusion",
                Priority = 3,

                
                OnConditionActivated = (Beast defender, Beast attacker) => {
                   
                    defender.confusionCounter = UnityEngine.Random.Range(1,5);
                    Beast.BattleDialog.Enqueue($"{Beast.FoeString(defender)} {defender.Name} was confused");
              
                    Debug.Log("Count Howdy ");
                    
                    defender.NewBeastConditions.Add(ConditionID.Confused);
                    
                },
                OnBeforeMove = (Beast attacker, Beast defender) =>
                {
                    Debug.Log("OnBeforeMove1 ");
                    int confusedNum;

                    //confusionCounter needs to be on the instance of beast to keep track of each beast's confusion turns
                    if (attacker.confusionCounter == 0/*beast.ConditionCounter == 0*/)
                    {
                        //confusedNum = 0;
                        Beast.BattleDialog.Enqueue($"{Beast.FoeString(attacker)} {attacker.Name} is no longer confused");
                        attacker.NewBeastConditions.Remove(ConditionID.Confused);
                        return false;
                    }else
                    {
                        Debug.Log("OnBeforeMove2 ");
                        confusedNum = UnityEngine.Random.Range(1, 3); 
                        
                        if(confusedNum == 2)
                        {
                            Beast.ConfusionDamage = true;
                            Beast.BattleDialog.Enqueue($"{Beast.FoeString(attacker)} {attacker.Name} is confused");
                            Beast.BattleDialog.Enqueue($"{Beast.FoeString(attacker)} {attacker.Name} hurt itself in confusion");
                            return true;
                        }
                        else
                        {
                            Beast.BattleDialog.Enqueue($"{Beast.FoeString(attacker)} {attacker.Name} is confused");
                            return false;
                        }
                    }
                },
                OnSecondaryEffect = (Beast defender, Beast attacker, Move moveUsed) =>
                {
                    int randNum= UnityEngine.Random.Range(1, 101);

                    if(randNum < moveUsed.SecondaryEffectChance)
                    {

                        Debug.Log("while confused ");
                        //if(!defender.NewBeastConditions.Contains(ConditionID.Confused)){
                            defender.NewBeastConditions.Add(ConditionID.Confused);
                            Beast.BattleDialog.Enqueue($"{Beast.FoeString(defender)} {defender.Name} was confused");
                            defender.confusionCounter = UnityEngine.Random.Range(1,5);
                        //}
                        
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
            new Condition(){
                
                Priority = 2,

                //change name to on conditionApplied seems better and then if fully confused the be Condition Activated
                OnConditionActivated = (Beast defender, Beast attacker) => {
                   
                    Debug.Log("Count Howdy ");

                    if((attacker.ModifiedStats[StatID.Speed] > defender.ModifiedStats[StatID.Speed]))
                    {
                        defender.NewBeastConditions.Add(ConditionID.Flinched);
                    }
                    
                    
                },
                OnBeforeMove = (Beast attacker, Beast defender) =>
                {
                    Debug.Log("OnBeforeMove1 ");
                    int confusedNum;

                    
                    
                        Debug.Log("OnBeforeMove2 ");
                        confusedNum = UnityEngine.Random.Range(1, 3); 
                        
                        Debug.Log("Speed atk " + attacker.ModifiedStats[StatID.Speed]);
                        Debug.Log("Speed def " + defender.ModifiedStats[StatID.Speed]);

                        Debug.Log("while contains flinch");
                        attacker.NewBeastConditions.Remove(ConditionID.Flinched);

                        if(confusedNum == 2)
                        {
                            
                            Beast.BattleDialog.Enqueue($"{Beast.FoeString(attacker)} {attacker.Name} flinched");
                            
                            return true;
                        }
                        else
                        {
                            
                            return false;
                        }
                    
                },
                OnSecondaryEffect = (Beast defender, Beast attacker, Move moveUsed) => {

                    Debug.Log("Count Howdy ");

                    if((attacker.ModifiedStats[StatID.Speed] > defender.ModifiedStats[StatID.Speed]))
                    {
                        defender.NewBeastConditions.Add(ConditionID.Flinched);
                    }


                },
                OnRemoveCondition = (Beast beast) =>
                {
                    beast.BeastConditions.Remove(ConditionID.Confused);
                },
            }
            
        },{
            ConditionID.Enamored,
            new Condition(){

                Priority = 4,

                OnConditionActivated = (Beast defender, Beast attacker) => {
                   
                    defender.NewBeastConditions.Add(ConditionID.Enamored);
                    Beast.BattleDialog.Enqueue($"{Beast.FoeString(defender)} {defender.Name} fell in love with{Beast.FoeString(attacker)} {attacker.Name}");
                },
                OnBeforeMove = (Beast attacker, Beast defender) =>
                {

                    int enamNum;

                        enamNum = UnityEngine.Random.Range(1, 3); 
                        
                        if(enamNum == 2)
                        {
                            Beast.BattleDialog.Enqueue($"{Beast.FoeString(attacker)} {attacker.Name} is in love");
                            Beast.BattleDialog.Enqueue($"{Beast.FoeString(attacker)} {attacker.Name} is immbolized by love");

                            return true;
                        }
                        else
                        {
                            Beast.BattleDialog.Enqueue($"{Beast.FoeString(attacker)} {attacker.Name} is in love");
                            return false;
                        }

                },
               
            }

        },


    };
}
