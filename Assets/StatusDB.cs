using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatusDB
{
    public static Dictionary<StatusID, Status> Statuses { get; set; } = new Dictionary<StatusID, Status>()
    {
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
                //Name = "Lustorm",
               
            }
        },

    };
}
