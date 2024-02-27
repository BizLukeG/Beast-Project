using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Area
{
    public int[] LevelRange { get; set; }
    public List<Func<int, Beast>> AvailableBeasts { get; set; }
    //string areaName;
    bool isInArea;
    public AreaID Id { get; set; }


    static public List<Func<int, Beast>> getRandomAvailableBeasts(int quantity)
    {
        List<Func<int, Beast>> BeastCreators = new List<Func<int, Beast>>();
        System.Random r = new System.Random();
        for (int i = 0; i < quantity; i++)
        {
            
            int rInt = r.Next(0, Enum.GetNames(typeof(BeastID)).Length);
            BeastCreators.Add(BeastCreatorDB.BeastCreators[(BeastID)rInt]);
        }

        return BeastCreators;
        
    }

}
