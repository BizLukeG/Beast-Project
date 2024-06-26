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

    static public Beast getBeastPerRoute(AreaID Route)
    {
        System.Random r = new System.Random();
        int rLevel = r.Next(AreaDB.Areas[Route].LevelRange[0], AreaDB.Areas[Route].LevelRange[1]);
        //needs to be changed for needs of specific routes (getrandomavailablebeasts)
        int rBeastIndex = r.Next(0, Enum.GetNames(typeof(BeastID)).Length);
        Debug.Log("NameIDLength " + Enum.GetNames(typeof(BeastID)).Length);
        Debug.Log("rBeastIndex " + rBeastIndex);
        Debug.Log("BeastID2 " + (BeastID)rBeastIndex);
        Debug.Log("BeastName " + BeastBaseDB.BeastBases[(BeastID)rBeastIndex].Name);
        Debug.Log("BeastAbility " + BeastBaseDB.BeastBases[(BeastID)rBeastIndex].Ability);
        Beast beast = new Beast(BeastID.Pugba, rLevel, BeastBaseDB.BeastBases[/*(BeastID)rBeastIndex*/BeastID.Pugba].MaxBaseStats);
        //BeastBaseDB.BeastBases[(BeastID)rBeastIndex].Name

        return beast;
    }

}
