using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Typing
{
   None, Rock, Aquatic, Corrupt , Mystic, Sacred, Glacial, Static, Nature, Native, Toxic, Hostile, Aerial
}

public class Beast
{
    //private by default
    public BeastID Name { get; set; }
    public int MaxBaseStats { get; set; }
    public int Level { get; set; }
    public Typing Typing1 { get; set; }
    public Typing Typing2 { get; set; }
    public List<MoveID> MoveSet { get; set; } = new List<MoveID>();
    public Dictionary<int, MoveID> LearnSet { get; set; }
    public BeastID beastid;
    public int maxBaseStats { get; set; }
    public BaseStatDistribution BaseStats { get; set; }
    public int[] Stats { get; set; }
    public int Att { get; set; }
    int def;
    int spAtt;
    public int SpDef { get; set; }
    int speed;
    

    public Beast()
    {

    }

    public Beast(BeastID name, int level, int maxBaseStats)
    {
        MaxBaseStats = maxBaseStats;
        Level = level;
        Name = name;
        createAllStats();
        createMoveSet(name, level);
        
    }

    //public Beast CreateNewBeast(string name, int level, int maxBaseStats)
    //{
    //    Beast beast = new Beast(name, level, maxBaseStats);
    //    return beast;
    //}

    public void createStats(BaseStatDistribution beastBaseStats, int level)
    {
        Stats = new int[5];
        Stats[0] = beastBaseStats.BaseAtt * 2 + level;
        Stats[1] = beastBaseStats.BaseDef * 2 + level;
        Stats[2] = beastBaseStats.BaseSpAtt * 2 + level;
        Stats[3] = beastBaseStats.BaseSpDef * 2 + level;
        Stats[4] = beastBaseStats.BaseSpeed * 2 + level;

        Level = level;
        Att = Stats[0];
        def = Stats[1];
        spAtt = Stats[2];
        SpDef = Stats[3];
        speed = Stats[4];

        //MoveSet.Add(MoveDB.Moves[LearnSet[1]]);
        //return stats;
    }

    public void createAllStats(){
        //Debug.Log("maxBaseStats " + MaxBaseStats);
        BaseStats = new BaseStatDistribution(MaxBaseStats);
        createStats(BaseStats, Level);
    }

    public void createMoveSet(BeastID name, int level){
        
        

        foreach (var kvp in BeastBaseDB.BeastBases[name].LearnSet)
        {
            Debug.Log("MoveSetCount " + MoveSet.Count);
            if (MoveSet.Count < 4)
            {
                if (kvp.Key <= level)
                {
                    MoveSet.Add(kvp.Value);
                }

            }
            else
            {
                System.Random r = new System.Random();
                int rInt = r.Next(0,4);
                MoveSet.RemoveAt(rInt);
                if (kvp.Key <= level)
                {
                    MoveSet.Add(kvp.Value);
                }
            }
            
        }
        //Debug.Log("MoveSet B " + MoveSet);
        foreach (var move in MoveSet)
        {
            Debug.Log("MoveSet B " + MoveDB.Moves[move].Name);
        }


        
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

        Debug.Log("Name " + Name);
        Debug.Log("Level " + Level);
        Debug.Log("MBS " + MaxBaseStats);
    }
}
