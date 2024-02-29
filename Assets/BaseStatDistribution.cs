using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class BaseStatDistribution
{
    int maxBaseStats;
    public int[] BaseStatsLimits { get; set; }
    public int[] ActualBaseStats { get; set; }
    //create getters for these
    public int BaseAtt { get; set; }
    public int BaseDef { get; set; }
    public int BaseSpAtt { get; set; }
    public int BaseSpDef { get; set; }
    public int BaseSpeed { get; set; }
    public int BaseHP { get; set; }
    System.Random rnd = new System.Random();

    int baseAccuracy;
    int baseEvasion;

    public BaseStatDistribution(int beastMaxBaseStats)
    {

        maxBaseStats = beastMaxBaseStats;
        BaseStatsLimits = createRandomlyDistributedBaseStatsMaxs(maxBaseStats);
        ActualBaseStats = createRandomActualBaseStats(BaseStatsLimits);
        BaseAtt = ActualBaseStats[0];
        BaseDef = ActualBaseStats[1];
        BaseSpAtt = ActualBaseStats[2];
        BaseSpDef = ActualBaseStats[3];
        BaseSpeed = ActualBaseStats[4];
        BaseHP = ActualBaseStats[5];

       
        

    }

    int[] createRandomlyDistributedBaseStatsMaxs(int maxBaseStats)
    {
        
        int[] baseStatsMaxs = new int[6]; ;
        
        int baseStatMaxMultiplier;
        for (int i = 0; i < 6; i++)
        {
            baseStatMaxMultiplier = 0;

            baseStatMaxMultiplier = rnd.Next(5, 11);
            
            baseStatsMaxs[i] = baseStatMaxMultiplier;
            Debug.Log("baseStatMaxMultiplier " + baseStatMaxMultiplier);
        }
        
        for (int i = 0; i < baseStatsMaxs.Length; i++)
        {
            baseStatsMaxs[i] = (int)Math.Round((baseStatsMaxs[i] /10f) * (maxBaseStats / 6f) );
        }

        foreach (int baseStatMax in baseStatsMaxs)
        {
            Debug.Log("baseStatMaxs: " + baseStatMax);

        }
        
        int baseStatsAdjustmentNum;
        int sumBaseStatsMaxs = baseStatsMaxs.Sum();
        Debug.Log("sumBaseStatsMaxs " + sumBaseStatsMaxs);
        if (sumBaseStatsMaxs != maxBaseStats)
            {
            baseStatsAdjustmentNum = maxBaseStats - sumBaseStatsMaxs;
        }
        else
        {
            baseStatsAdjustmentNum = 0;
        }
        Debug.Log("baseStatsAdjustmentNum " + baseStatsAdjustmentNum);
        //Makes baseStateMaxs adjustments using the Previously calculated adjustment number
        int maxAdjustment = (int)Math.Round(maxBaseStats / 5 * 1.5, MidpointRounding.AwayFromZero);
        int statAdjusted;
        Debug.Log("max Adj: " + maxAdjustment);

        do
        {
            statAdjusted = rnd.Next(0, 6);
            baseStatsMaxs[statAdjusted] += 1;
            if (baseStatsMaxs[statAdjusted] > maxAdjustment)
            {
                baseStatsAdjustmentNum += 1;
                baseStatsMaxs[statAdjusted] -= 1;
            }
            else {
                baseStatsAdjustmentNum -= 1;
            }
            
        } while (baseStatsAdjustmentNum != 0);
        
        foreach (int baseStatMax in baseStatsMaxs)
        {
            Debug.Log("baseStatMaxsAfterAdjustment: " + baseStatMax);

        }

        
        return baseStatsMaxs;
    }

    int[] createRandomActualBaseStats(int[] baseStatsLimits)
    {
        
        int[] actualBaseStatsLimits = new int[baseStatsLimits.Length];
       
        for (int i = 0; i < baseStatsLimits.Length; i++)
        {
            float multiplier = (rnd.Next(5, 11) / 10f);
            Debug.Log("multi: " + multiplier);
            actualBaseStatsLimits[i] = (int)Math.Round(baseStatsLimits[i] * multiplier, MidpointRounding.AwayFromZero);
        }
       
     
        return actualBaseStatsLimits;
    }
}
