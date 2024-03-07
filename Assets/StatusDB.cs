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
            StatusID.Poisoned,
            new Status(){
                //Name = "Pugba",
                
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
