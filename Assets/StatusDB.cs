using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatusDB
{
    public static Dictionary<StatusID, Status> Statuses { get; set; } = new Dictionary<StatusID, Status>()
    {

        //Beast have multiple conditions and can have conditions and statuses, but can't have multiple statuses at the same time

        //beastBase to take values from 
        {
            StatusID.Burned,
            new Status(){
                Priority = 5,
                OnStatusActivated = (Beast defender) => {
                    defender.NewBeastStatuses.Add(StatusID.Burned);
                    defender.ModifiedStats[StatID.Attack] = (int)Math.Round(.5 * defender.ModifiedStats[StatID.Attack], MidpointRounding.AwayFromZero);
                    Beast.BattleDialog.Enqueue($"{Beast.FoeString(defender)} {defender.Name} was burned");
                    defender.AfterTurnDamage = (int)Math.Round(defender.Stats[StatID.HP]*(1/8f), MidpointRounding.AwayFromZero);
                    defender.AfterTurnDamageName = "burn";
                },
                OnBeforeMove = (Beast attacker) =>
                {
                        return false;                  
                },
                OnSecondaryEffect = (Beast defender, Beast attacker, Move moveUsed) =>
                {
                    int randNum = UnityEngine.Random.Range(1, 101);
                    
                    if(randNum <= moveUsed.SecondaryEffectChance)
                    {
                        
                        Debug.Log("while burned ");
                        //if(defender.NewBeastStatuses.Count == 0){
                            defender.NewBeastStatuses.Add(StatusID.Burned);
                            defender.ModifiedStats[StatID.Attack] = (int)Math.Round(.5 * defender.ModifiedStats[StatID.Attack], MidpointRounding.AwayFromZero);
                            Beast.BattleDialog.Enqueue($"{Beast.FoeString(defender)} {defender.Name} was burned");
                            defender.AfterTurnDamage = (int)Math.Round(defender.Stats[StatID.HP]*(1/8f), MidpointRounding.AwayFromZero);
                            defender.AfterTurnDamageName = "burn";
                        //}
                    }
                },
            }
        },
        {
            StatusID.Paralyzed,
            new Status(){
                Priority = 5,

                OnStatusActivated = (Beast defender) => {

                    defender.NewBeastStatuses.Add(StatusID.Paralyzed);
                    defender.ModifiedStats[StatID.Speed] = (int)Math.Round(.5 * defender.ModifiedStats[StatID.Speed], MidpointRounding.AwayFromZero);
                    Beast.BattleDialog.Enqueue($"{Beast.FoeString(defender)} {defender.Name} was paralyzed");
                },
                OnBeforeMove = (Beast attacker) =>
                {
                    int fullParaNum = UnityEngine.Random.Range(1, 3);

                    if(fullParaNum == 2)
                    {
                        Beast.BattleDialog.Enqueue($"{Beast.FoeString(attacker)} {attacker.Name} is fully paralyzed");
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
        },
        {
            StatusID.Asleep,
            new Status(){
                ActivationMessage = "was put to sleep.",
                Priority = 1,

                OnStatusActivated = (Beast defender) => {
                    //beast.Status = StatusID.Paralyzed;
                    defender.NewStatusCounter = UnityEngine.Random.Range(1,5);
                    Debug.Log("statCounter def " + defender.NewStatusCounter);

                    defender.NewBeastStatuses.Add(StatusID.Asleep);

                    Beast.BattleDialog.Enqueue($"{Beast.FoeString(defender)} {defender.Name} was put to sleep");

                },
                OnBeforeMove = (Beast attacker) =>
                {
                   Debug.Log("statCounter att " + attacker.NewStatusCounter);

                    if (attacker.NewStatusCounter == 0)
                    {
                       
                        Beast.BattleDialog.Enqueue($"{Beast.FoeString(attacker)} {attacker.Name} woke up");
                        attacker.NewBeastStatuses.Remove(StatusID.Asleep);
                        return false;
                    }else
                    {
                                                
                            Beast.BattleDialog.Enqueue($"{Beast.FoeString(attacker)} {attacker.Name} is deeply asleep");
                            return true;
                        
                    }
                }

            }
        },
        {
            StatusID.Frozen,
            new Status(){
                
                Priority = 1,

                OnStatusActivated = (Beast defender) => {
                    //beast.Status = StatusID.Paralyzed;
                    defender.NewStatusCounter = UnityEngine.Random.Range(1,5);
                    Debug.Log("statCounter def " + defender.NewStatusCounter);

                    defender.NewBeastStatuses.Add(StatusID.Frozen);

                    Beast.BattleDialog.Enqueue($"{Beast.FoeString(defender)} {defender.Name} was Frozen");

                },
                OnBeforeMove = (Beast attacker) =>
                {
                   Debug.Log("statCounter att " + attacker.NewStatusCounter);

                    if (attacker.NewStatusCounter == 0)
                    {

                        Beast.BattleDialog.Enqueue($"{Beast.FoeString(attacker)} {attacker.Name} thawed");
                        attacker.NewBeastStatuses.Remove(StatusID.Frozen);
                        return false;
                    }else
                    {

                            Beast.BattleDialog.Enqueue($"{Beast.FoeString(attacker)} {attacker.Name} is frozen solid");
                            return true;

                    }
                }

            }
        },
        {
            StatusID.Poisoned,
            new Status(){
                Priority = 5,
                OnStatusActivated = (Beast defender) => {
                    defender.NewBeastStatuses.Add(StatusID.Poisoned);
                    defender.AfterTurnDamage = (int)Math.Round(defender.Stats[StatID.HP]*(1/8f), MidpointRounding.AwayFromZero);
                    Beast.BattleDialog.Enqueue($"{Beast.FoeString(defender)} {defender.Name} was poisoned");
                    defender.AfterTurnDamageName = "poison";
                },
                OnBeforeMove = (Beast attacker) =>
                {
                        return false;
                },
                OnSecondaryEffect = (Beast defender, Beast attacker, Move moveUsed) =>
                {
                    int randNum = UnityEngine.Random.Range(1, 101);

                    if(randNum <= moveUsed.SecondaryEffectChance)
                    {

                        Debug.Log("while poisoned ");
                        //if(defender.NewBeastStatuses.Count == 0){
                            defender.NewBeastStatuses.Add(StatusID.Poisoned);                          
                            Beast.BattleDialog.Enqueue($"{Beast.FoeString(defender)} {defender.Name} was poisoned");
                            defender.AfterTurnDamage = (int)Math.Round(defender.Stats[StatID.HP]*(1/8f), MidpointRounding.AwayFromZero);
                            defender.AfterTurnDamageName = "poison";
                        //}
                    }
                },
            }
        },


    };
}
