using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Area
{
    public int[] LevelRange { get; set; }
    public List<Beast> AvailableBeasts { get; set; }
    //string areaName;
    bool isInArea;
    public AreaID Id { get; set; }


    static public List<Beast> getRandomAvailableBeasts(int quantity)
    {
        List<Beast> BeastCreators = new List<Beast>();
        System.Random r = new System.Random();
        for (int i = 0; i < quantity; i++)
        {
            
            int rInt = r.Next(0, Enum.GetNames(typeof(BeastID)).Length);
            BeastCreators.Add(BeastBaseDB.BeastBases[(BeastID)rInt]);
        }

        return BeastCreators;
        
    }

}
