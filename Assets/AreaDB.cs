using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum AreaID
{
    Route101, Route102, Route103, Route104, Route105, Route106, Route107,
}

public class AreaDB
{

    public static void Init()
    {
        foreach (var kvp in Areas)
        {
            var areaId = kvp.Key;
            var area = kvp.Value;

            area.Id = areaId;
            
        }
    }

    public static Dictionary<AreaID, Area> Areas { get; set; } = new Dictionary<AreaID, Area>()
    {
        {
            AreaID.Route101, 
            new Area(){
                LevelRange = new int[] {2,4},
                AvailableBeasts = Area.getRandomAvailableBeasts(4),
            }
        }
    };
}
