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
                ActivationMessage = "has been burned.",
                //Name = "Expurn",
                //FrontSprite = Resources.Load<Sprite>("Sprites/Brown"),
                OnStatusActivated = (Beast beast) => {
                    beast.Status = StatusID.Burned;
                    beast.ModifiedStats[StatID.Attack] = (int)Math.Round(.5 * beast.ModifiedStats[StatID.Attack], MidpointRounding.AwayFromZero);
                },
            }
        },
        {
            StatusID.Paralyzed,
            new Status(){
                //Name = "Pugba",
                ActivationMessage = "has been paralyzed",
                BeforeTurnMessage = "was fully paralyzed",
                OnStatusActivated = (Beast beast) => {
                    beast.Status = StatusID.Paralyzed;
                    beast.ModifiedStats[StatID.Speed] = (int)Math.Round(.5 * beast.ModifiedStats[StatID.Speed], MidpointRounding.AwayFromZero);
                },
                OnBeforeMove = (Beast beast) =>
                {
                    int fullParaNum = UnityEngine.Random.Range(1, 3);
                    
                    if(fullParaNum == 2)
                    {
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

    };
}
