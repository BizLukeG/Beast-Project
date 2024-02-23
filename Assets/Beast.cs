using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Beast
{
    //private by default
    string name;
    int level;
    int maxBaseStats;
    public BaseStatDistribution baseStats;
    public int[] stats;
    public int att;
    int def;
    int spAtt;
    public int spDef;
    int speed;

    public Beast(/*int level*/)
    {
        
        level = 20;
        maxBaseStats = 300;
        baseStats = new BaseStatDistribution(maxBaseStats);
        stats = createStats(baseStats);
        att = stats[0];
        def = stats[1];
        spAtt = stats[2];
        spDef = stats[3];
        speed = stats[4];
    }

    int[] createStats(BaseStatDistribution beastBaseStats)
    {
        int[] stats = new int[5];
        stats[0] = beastBaseStats.baseAtt * 2;
        stats[1] = beastBaseStats.baseDef;
        stats[2] = beastBaseStats.baseSpAtt;
        stats[3] = beastBaseStats.baseSpDef * 2;
        stats[4] = beastBaseStats.baseSpeed;
        return stats;
    }
}
