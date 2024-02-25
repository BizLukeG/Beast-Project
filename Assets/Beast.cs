using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Beast
{
    //private by default
    string name;
    //public int Level { get; set; }
    public int maxBaseStats { get; set; }
    public BaseStatDistribution BaseStats { get; set; }
    public int[] Stats { get; set; }
    public int Att { get; set; }
    int def;
    int spAtt;
    public int SpDef { get; set; }
    int speed;

    public Beast(/*int level*/)
    {
        //move into functions that update the beast properties on their own
        //level = 20;
        //maxBaseStats = 300;
        //BaseStats = new BaseStatDistribution(maxBaseStats);
        //Stats = createStats(BaseStats);
        //Att = Stats[0];
        //def = Stats[1];
        //spAtt = Stats[2];
        //SpDef = Stats[3];
        //speed = Stats[4];
    }

    public void createStats(BaseStatDistribution beastBaseStats)
    {
        Stats = new int[5];
        Stats[0] = beastBaseStats.BaseAtt * 2;
        Stats[1] = beastBaseStats.BaseDef * 2;
        Stats[2] = beastBaseStats.BaseSpAtt * 2;
        Stats[3] = beastBaseStats.BaseSpDef * 2;
        Stats[4] = beastBaseStats.BaseSpeed * 2;

        Att = Stats[0];
        def = Stats[1];
        spAtt = Stats[2];
        SpDef = Stats[3];
        speed = Stats[4];
        //return stats;
    }

    public void createAllStats(int maxBaseStats){
        BaseStats = new BaseStatDistribution(maxBaseStats);
        createStats(BaseStats);
    }

     public void CheckAllStats()
    {
        Debug.Log("Stats: ");
        foreach (var stat in this.Stats)
        {
            Debug.Log(stat);
        }

        Debug.Log("BaseStats: ");
        foreach (var baseStat in this.BaseStats.ActualBaseStats)
        {
            Debug.Log(baseStat);
        }

        Debug.Log("BaseStatsLimits: ");
        foreach (var baseStatLimit in this.BaseStats.BaseStatsLimits)
        {
            Debug.Log(baseStatLimit);
        }
    }
}
