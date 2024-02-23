using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class BaseStatDistribution
{
    int maxBaseStats;
    int[] baseStatsLimits;
    int[] actualBaseStats;
    //create getters for these
    public int baseAtt;
    public int baseDef;
    public int baseSpAtt;
    public int baseSpDef;
    public int baseSpeed;

    int baseAccuracy;
    int baseEvasion;

    public BaseStatDistribution(int beastMaxBaseStats)
    {

        maxBaseStats = beastMaxBaseStats;
        //this.System.rnd = new Random();
        baseStatsLimits = createRandomlyDistributedBaseStatsMaxs(maxBaseStats);
        actualBaseStats = createRandomActualBaseStats(baseStatsLimits);
        baseAtt = actualBaseStats[0];
        baseDef = actualBaseStats[1];
        baseSpAtt = actualBaseStats[2];
        baseSpDef = actualBaseStats[3];
        baseSpeed = actualBaseStats[4];

       
        

    }

    int[] createRandomlyDistributedBaseStatsMaxs(int maxBaseStats)
    {
        System.Random rnd = new System.Random();
        //Makes orginal random baseStateMaxs
        int[] baseStatsMaxs = new int[5]; ;
        //make baseStatsMaxs here and push in function then add to constructor after pushed
        int baseStatMaxMultiplier;
        for (int i = 0; i < 5; i++)
        {
            baseStatMaxMultiplier = 0;

            baseStatMaxMultiplier = rnd.Next(5, 11);
            
            baseStatsMaxs[i] = baseStatMaxMultiplier;
        }
        //Finds the Adjustment Number
        for (int i = 0; i == baseStatsMaxs.Length; i++)
        {
            baseStatsMaxs[i] = (int)Math.Round(baseStatsMaxs[i] * (rnd.Next(5, 11) / 10f), MidpointRounding.AwayFromZero);
        }
        //foreach (int baseStatMax in baseStatsMaxs)
        //{
        //    baseStatMax / 10 *= this.maxBaseStats / 5;

        //}
        Debug.Log("baseStatsMaxs " + baseStatsMaxs[0]);
        Debug.Log("baseStatsMaxs " + baseStatsMaxs[3]);
        int baseStatsAdjustmentNum;
        int sumBaseStatsMaxs = baseStatsMaxs.Sum();
        if (sumBaseStatsMaxs != maxBaseStats)
            {
            baseStatsAdjustmentNum = maxBaseStats - sumBaseStatsMaxs;
        }
        else
        {
            baseStatsAdjustmentNum = 0;
        }

        //Makes baseStateMaxs adjustments using the Previously calculated adjustment number
        int maxAdjustment = (int)Math.Round(maxBaseStats / 5 * 1.5, MidpointRounding.AwayFromZero);
        int statAdjusted;
        Console.WriteLine("max Adj:" + maxAdjustment);

        do
        {
            statAdjusted = rnd.Next(0, 5);
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

        Debug.Log("baseStatsMaxs2 " + baseStatsMaxs[0]);
        Debug.Log("baseStatsMaxs2 " + baseStatsMaxs[3]);
        return baseStatsMaxs;
    }

    int[] createRandomActualBaseStats(int[] baseStatsLimits)
    {
        System.Random rnd = new System.Random();
        int[] actualBaseStatsLimits = new int[baseStatsLimits.Length];
        Debug.Log("baseStatsLimits: " + baseStatsLimits[0]);
        float multiplier = (rnd.Next(5, 11) / 10f);
        Debug.Log("multi: " + multiplier);        
        Debug.Log("Calc " + (baseStatsLimits[0] * multiplier));
        for (int i = 0; i < baseStatsLimits.Length; i++)
        {

            actualBaseStatsLimits[i] = (int)Math.Round(baseStatsLimits[i] * multiplier, MidpointRounding.AwayFromZero);
        }
        Debug.Log("actualBaseStats: " + actualBaseStatsLimits[0]);
     
        return actualBaseStatsLimits;
    }
}
