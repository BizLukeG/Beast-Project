using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public enum StatID
{
    HP, Attack, Defense, SpecialAttack, SpecialDefense, Speed
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
    //public int[] Stats { get; set; }
    public Dictionary<StatID, int> Stats { get; set; }
    public Dictionary<StatID, int> ModifiedStats { get; set; }
    //public int Att { get; set; }
    //public int Def { get; set; }
    //public int SpAtt { get; set; }
    //public int SpDef { get; set; }
    //public int Speed { get; set; }
    //public int HP { get; set; }
    //public int CurrentAtt { get; set; }
    //public int CurrentDef { get; set; }
    //public int CurrentSpAtt { get; set; }
    //public int CurrentSpDef { get; set; }
    //public int CurrentSpeed { get; set; }
    //public int CurrentHP { get; set; }
    public Sprite FrontSprite { get; set; } //= Resources.Load<Sprite>("Sprites/Brown");
    public bool IsPlayerUnit { get; set; } = false;
    public StatusID Status { get; set; } = StatusID.None;
    public ConditionID Condition { get; set; } = ConditionID.None;
    public int StatusCounter { get; set; } = 0;
    public int ConditionCounter { get; set; } = 0;
    public static Queue<string> BattleDialog { get; set; } = new Queue<string>();
    public static ArrayList StaticPriorityArrList = new ArrayList();
    public List<ConditionID> BeastConditions { get; set; } = new List<ConditionID>();
    public List<ConditionID> NewBeastConditions { get; set; } = new List<ConditionID>();
    public List<StatusID> NewBeastStatuses { get; set; } = new List<StatusID>();
    public static bool statusConditionActivated = false;
    public int confusionCounter = 0;
    public List<ConditionID> TempNewBeastConditions { get; set; } = new List<ConditionID>();
    public List<StatusID> TempNewBeastStatuses { get; set; } = new List<StatusID>();
    public int NewStatusCounter { get; set; } = 0;

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
        FrontSprite = Resources.Load<Sprite>($"Sprites/{name.ToString()} FrontSprite");

    }

    //public Beast CreateNewBeast(string name, int level, int maxBaseStats)
    //{
    //    Beast beast = new Beast(name, level, maxBaseStats);
    //    return beast;
    //}

    public void createStats(BaseStatDistribution beastBaseStats, int level)
    {
        //Stats = new int[6];
        //Stats[0] = beastBaseStats.BaseAtt * 2 + level;
        //Stats[1] = beastBaseStats.BaseDef * 2 + level;
        //Stats[2] = beastBaseStats.BaseSpAtt * 2 + level;
        //Stats[3] = beastBaseStats.BaseSpDef * 2 + level;
        //Stats[4] = beastBaseStats.BaseSpeed * 2 + level;
        //Stats[5] = beastBaseStats.BaseHP * 2 + level;

        Stats = new Dictionary<StatID, int>()
        {
            {StatID.HP, beastBaseStats.BaseHP * 2 + level},
            {StatID.Attack, beastBaseStats.BaseAtt * 2 + level},
            {StatID.Defense, beastBaseStats.BaseDef * 2 + level},
            {StatID.SpecialAttack, beastBaseStats.BaseSpDef * 2 + level},
            {StatID.SpecialDefense, beastBaseStats.BaseSpAtt * 2 + level},
            {StatID.Speed, beastBaseStats.BaseSpeed * 2 + level}

        };

        ModifiedStats = new Dictionary<StatID, int>()
        {
            {StatID.HP, Stats[StatID.HP]},
            {StatID.Attack, Stats[StatID.Attack]},
            {StatID.Defense, Stats[StatID.Defense]},
            {StatID.SpecialAttack, Stats[StatID.SpecialAttack]},
            {StatID.SpecialDefense, Stats[StatID.SpecialDefense]},
            {StatID.Speed, Stats[StatID.Speed]}

        };

        Level = level;


        //HP = Stats[StatID.HP];
        //Att = Stats[StatID.Attack];
        //Def = Stats[StatID.Defense];
        //SpAtt = Stats[StatID.SpecialAttack];
        //SpDef = Stats[StatID.SpecialDefense];
        //Speed = Stats[StatID.Speed];


        //CurrentHP = HP;
        //CurrentAtt = Att;
        //CurrentDef = Def;
        //CurrentSpAtt = SpAtt;
        //CurrentSpDef = SpDef;
        //CurrentSpeed = Speed;


        //MoveSet.Add(MoveDB.Moves[LearnSet[1]]);
        //return stats;
    }

    public void createAllStats() {
        //Debug.Log("maxBaseStats " + MaxBaseStats);
        BaseStats = new BaseStatDistribution(MaxBaseStats);
        createStats(BaseStats, Level);
    }

    public void createMoveSet(BeastID name, int level) {



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
                int rInt = r.Next(0, 4);
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



    public void ResetStats()
    {
        int HP = ModifiedStats[StatID.HP];
        foreach (var kvp in Stats)
        {
            ModifiedStats[kvp.Key] = Stats[kvp.Key];
        }
        ModifiedStats[StatID.HP] = HP;
    }

    //public void CheckAllStats()
    //{
    //    Debug.Log("Stats: ");
    //    foreach (var stat in this.Stats)
    //    {
    //        Debug.Log(stat);
    //    }

    //    Debug.Log("BaseStats: ");
    //    foreach (var baseStat in this.BaseStats.ActualBaseStats)
    //    {
    //        Debug.Log(baseStat);
    //    }

    //    Debug.Log("BaseStatsLimits: ");
    //    foreach (var baseStatLimit in this.BaseStats.BaseStatsLimits)
    //    {
    //        Debug.Log(baseStatLimit);
    //    }

    //    Debug.Log("Name " + Name);
    //    Debug.Log("Level " + Level);
    //    Debug.Log("MBS " + MaxBaseStats);
    //}



    public static float DamageCalc(Move moveUsed, Beast firstUnitToMove, Beast secondUnitToMove, bool firstMove)
    {
        Beast attacker; Beast defender;
        if (firstMove)
        {
            attacker = firstUnitToMove;
            defender = secondUnitToMove;
        }
        else
        {
            attacker = secondUnitToMove;
            defender = firstUnitToMove;
        }

        int damage = 0;
        float effectiveness = 1;
        //int confusedNum = 0;
        bool IsBeforeMoveActivated = false;
        Debug.Log("attackerCondition " + attacker.Condition);
        ConditionID ActivatedCondition = ConditionID.None;
        StatusID ActivatedStatus = StatusID.None;
        List<ConditionID> sortedConditions = new List<ConditionID>();
        //ArrayList sortedConditions = new ArrayList();
        List<int> RandomNums = new List<int>();
        RandomNums.Add(4); RandomNums.Add(7); RandomNums.Add(2);

        int Response = RandomNums.Aggregate((smallest, next) =>
        next < smallest ? next : smallest
        );

        attacker.TempNewBeastConditions.Clear();
        attacker.TempNewBeastStatuses.Clear();
        //attacker.TempNewBeastConditions = attacker.NewBeastConditions.Select(x => x);
        attacker.NewBeastStatuses.ForEach((x) =>
        {
            attacker.TempNewBeastStatuses.Add(x);
        });
        attacker.NewBeastConditions.ForEach((x) =>
        {
            attacker.TempNewBeastConditions.Add(x);
        });

        Debug.Log("Count C" + attacker.TempNewBeastConditions.Count);
        Debug.Log("Count S" + attacker.TempNewBeastStatuses.Count);

        //loop to find smallest priority of condition/status and call its beforemove(). if its beforemove is activated to skip the turn then exit out of loop else find next smallest priority etc until 
        //no more conditions or statuses are left
        //might need to have a BeastConditions List and then a TempBeastConditionsList. The Temp list used in the while loop to remove conditions and statuses until none are left so that orginal isn't
        //affected by the removal process in the loop
        while (!statusConditionActivated && attacker.TempNewBeastConditions.Count > 0 || attacker.TempNewBeastStatuses.Count > 0 /*&& !statusConditionActivated*/) {
            //check for lowest prio in attacker's BeastConditions
            Debug.Log("Count Con" + attacker.TempNewBeastConditions.Count);
            Debug.Log("Count Stat" + attacker.TempNewBeastStatuses.Count);
            int conditionPrio;
            int statusPrio;
            StatusID prioStatusResponse = StatusID.Burned;
            ConditionID prioConditionResponse = ConditionID.Enamored;
            if (attacker.TempNewBeastConditions.Count > 0)
            {
                prioConditionResponse = attacker.TempNewBeastConditions.Aggregate((smallest, next) => ConditionDB.Conditions[next].Priority < ConditionDB.Conditions[smallest].Priority ? next : smallest);
                conditionPrio = ConditionDB.Conditions[prioConditionResponse].Priority;
            }
            else
            {
                conditionPrio = 100;
            }
            if (attacker.TempNewBeastStatuses.Count > 0)
            {
                prioStatusResponse = attacker.TempNewBeastStatuses.Aggregate((smallest, next) => StatusDB.Statuses[next].Priority < StatusDB.Statuses[smallest].Priority ? next : smallest);
                statusPrio = StatusDB.Statuses[prioStatusResponse].Priority;
            }
            else
            {
                statusPrio = 100;
            }
            //need to check status for lowest prio also then compare the results PrioResponse of Condition and Statuses to see which is smaller and continue the process after
            //StatusID PrioStatusResponse = attacker.TempNewBeastStatuses.Aggregate((smallest, next) => StatusDB.Statuses[next].Priority < StatusDB.Statuses[smallest].Priority ? next : smallest);
            Debug.Log("conditionPrio" + conditionPrio);
            Debug.Log("statusPrio" + statusPrio);

            if (conditionPrio > statusPrio) {
                //StatusDB.Statuses[PrioStatusResponse].Priority < ConditionDB.Conditions[PrioResponse].Priority

                //could set statusConditionActivated = to this since it returns a bool
                //see if condition fully activates
                Debug.Log("prio " + prioStatusResponse);
                statusConditionActivated = StatusDB.Statuses[prioStatusResponse].OnBeforeMove(attacker);

                //remove the condition so that the next loop can check the next lowest prio
                attacker.TempNewBeastStatuses.Remove(prioStatusResponse);
            }
            else
            {
                //see if condition fully activates
                statusConditionActivated = ConditionDB.Conditions[prioConditionResponse].OnBeforeMove(attacker, defender);
                Debug.Log("prio " + prioConditionResponse);
                //remove the condition so that the next loop can check the next lowest prio
                attacker.TempNewBeastConditions.Remove(prioConditionResponse);
            }

            attacker.NewBeastConditions.Remove(ConditionID.Flinched);
            //if statusConditionActivated = true (set from condition/status database) then loop will exit
        }
        

        Debug.Log("Response " + Response);

        //check beast conditions and statuses to see if it gets to do its move or not

        //ConditionDB.Conditions[sortedCondition].OnBeforeMove(attacker);



        //foreach (var beastCondition in attacker.BeastConditions)
        //{
        //    Debug.Log("beastCondition " + beastCondition);
        //}

        //if (attacker.BeastConditions.Count > 0)
        //{

        //    List<int> priorities = new List<int>();
        //    foreach (var condition in attacker.BeastConditions)
        //    {
        //        priorities.Add(ConditionDB.Conditions[condition].Priority);
        //    }


        //    List<int> sortedPriorities = new List<int>();
        //    for (int i = 1; i < 7; i++)
        //    {
        //        foreach (var priority in priorities)
        //        {
        //            Debug.Log("conditionPriority " + priority);
        //            if (priority == i)
        //            {
        //                sortedPriorities.Add(priority);
        //                break;
        //            }
        //        }
        //    }

        //    //List<ConditionID> sortedConditions = new List<ConditionID>();
        //    foreach (var sortedPriority in sortedPriorities)
        //    {
        //        Debug.Log("sortedConditionPriority " + sortedPriority);
        //        foreach (var condition in attacker.BeastConditions)
        //        {
        //            if (ConditionDB.Conditions[condition].Priority == sortedPriority)
        //            {
        //                sortedConditions.Add(condition);
        //                break;
        //            }
        //        }

        //    }
        //    foreach (var sortedCondition in sortedConditions)
        //    {
        //        if (ConditionDB.Conditions[sortedCondition].OnBeforeMove(attacker))
        //        {
        //            ActivatedCondition = sortedCondition;
        //            break;
        //        }
        //    }


        //}

        //foreach (var sortedCondition in sortedConditions)
        //{
        //    Debug.Log("sortedCondition " + sortedCondition);
        //}

        //Debug.Log("activatedCondition " + ActivatedCondition);

        ////check Sleep / Frozen
        //if (ActivatedStatus == StatusID.Asleep)
        //{
        //    BattleDialog.Enqueue($"{FoeString(attacker)} {attacker.Name} {StatusDB.Statuses[ActivatedStatus].BeforeTurnMessage}");
        //}


        //if (sortedConditions.Contains(ConditionID.Confused))
        //{

        //    //BattleDialog.Enqueue($"{FoeString(attacker)} {attacker.Name} {ConditionDB.Conditions[ConditionID.Confused].CurrentlyConfusedMessage}");

        //    if (attacker.ConditionCounter == 0)
        //    {
        //        ConditionDB.Conditions[ConditionID.Confused].OnRemoveCondition(attacker);
        //        BattleDialog.Enqueue($"{FoeString(attacker)} {attacker.Name} snapped out of Confusion");
        //    }
        //    else
        //    {
        //        BattleDialog.Enqueue($"{FoeString(attacker)} {attacker.Name} {ConditionDB.Conditions[ConditionID.Confused].CurrentlyConfusedMessage}");
        //        attacker.ConditionCounter--;
        //    }

        //}

        //if (sortedConditions.Contains(ConditionID.Enamored))
        //{
        //    BattleDialog.Enqueue($"{FoeString(attacker)} {attacker.Name} {ConditionDB.Conditions[ConditionID.Enamored].CurrentlyEnamoredMessage}");
        //}


        //if(ActivatedStatus != StatusID.None)
        //{
        //    if(ActivatedStatus == StatusID.Paralyzed)
        //    {
        //        BattleDialog.Enqueue($"{FoeString(attacker)} {attacker.Name} {StatusDB.Statuses[ActivatedStatus].BeforeTurnMessage}");
        //    }

        //}
        //else if (ActivatedCondition != ConditionID.None)
        //{
        //    if(ActivatedCondition == ConditionID.Flinched && attacker == secondUnitToMove)
        //    {
        //        BattleDialog.Enqueue($"{FoeString(attacker)} {attacker.Name} {ConditionDB.Conditions[ActivatedCondition].FullyFlinchedMessage}");
        //        ConditionDB.Conditions[ActivatedCondition].OnRemoveCondition(attacker);
        //    }

        //    if(ActivatedCondition == ConditionID.Confused)
        //    {



        //            BattleDialog.Enqueue($"{FoeString(attacker)} {attacker.Name} {ConditionDB.Conditions[ActivatedCondition].FullyConfusedMessage}");

        //            if (moveUsed.Category == MoveCategory.Physical)
        //            {
        //                damage = (int)Math.Round(moveUsed.Power / 100f * (attacker.ModifiedStats[StatID.Attack] - attacker.ModifiedStats[StatID.Defense]) * effectiveness, MidpointRounding.AwayFromZero);
        //            }
        //            else
        //            {

        //                damage = (int)Math.Round(moveUsed.Power / 100f * (attacker.ModifiedStats[StatID.SpecialAttack] - attacker.ModifiedStats[StatID.SpecialDefense]) * effectiveness, MidpointRounding.AwayFromZero);
        //                //Debug.Log($"confusion damage breakdown: Power {moveUsed.Power}, SpA: {attacker.ModifiedStats[StatID.SpecialAttack]}, SpD ");
        //            }

        //            Debug.Log("confusion damage " + damage);

        //            if (damage <= 0)
        //            {
        //                damage = 1;
        //            }


        //    }
        //}
        //else
        //{

        //if statusConditionActivated is true skip turn essentailly
        if (!statusConditionActivated)
        {
            BattleDialog.Enqueue($"{FoeString(attacker)} {attacker.Name} used {moveUsed.Name}");


            if (moveUsed.Category == MoveCategory.Status)
            {
                if (!defender.NewBeastStatuses.Contains(moveUsed.Status)/*defender.Status == StatusID.None*/)
                {
                    //defender.Status = moveUsed.Status;
                    StatusDB.Statuses[moveUsed.Status].OnStatusActivated(defender);
                    //StatusDB.Statuses[defender.Status].OnStatusActivated(defender);
                    //BattleDialog.Enqueue($"{FoeString(defender)} {defender.Name} {StatusDB.Statuses[defender.Status].ActivationMessage}");
                }
                else
                {
                    BattleDialog.Enqueue($"{FoeString(defender)} {defender.Name} is already {defender.Status.ToString()}");
                }
            }
            else if (moveUsed.Category == MoveCategory.Condition)
            {
                if (!defender.NewBeastConditions.Contains(moveUsed.Condition)) //!defender.BeastConditions.Contains(moveUsed.Condition))
                {
                    Debug.Log("Count Activated ");
                    ConditionDB.Conditions[moveUsed.Condition].OnConditionActivated(defender, attacker);

                    //ConditionDB.Conditions[defender.NewBeastConditions[defender.BeastConditions.Count - 1]].OnConditionActivated(defender);

                    //defender.BeastConditions.Add(moveUsed.Condition);

                    //if (moveUsed.Condition != ConditionID.Flinched)
                    //{
                    //ConditionDB.Conditions[defender.BeastConditions[defender.BeastConditions.Count - 1]].OnConditionActivated(defender);
                    //BattleDialog.Enqueue($"{FoeString(defender)} {defender.Name} {ConditionDB.Conditions[defender.BeastConditions[defender.BeastConditions.Count - 1]].ActivationMessage}");
                    //}
                }
                else
                {
                    BattleDialog.Enqueue($"{FoeString(defender)} {defender.Name} is already {moveUsed.Condition.ToString()}");
                }

            }
            else if (moveUsed.Category == MoveCategory.ModifyStats)
            {


                if (moveUsed.TargetSelf == true)
                {

                    foreach (var buffedStat in moveUsed.BuffedStats)
                    {
                        attacker.ModifiedStats[buffedStat] += (int)Math.Round(.5 * attacker.ModifiedStats[buffedStat], MidpointRounding.AwayFromZero);
                    }
                    foreach (var nerfedStat in moveUsed.NerfedStats)
                    {
                        attacker.ModifiedStats[nerfedStat] -= (int)Math.Round(.5 * attacker.ModifiedStats[nerfedStat], MidpointRounding.AwayFromZero);
                    }

                }
                else if (moveUsed.TargetSelf == false)
                {
                    foreach (var buffedStat in moveUsed.BuffedStats)
                    {
                        defender.ModifiedStats[buffedStat] += (int)Math.Round(.5 * defender.ModifiedStats[buffedStat], MidpointRounding.AwayFromZero);
                    }
                    foreach (var nerfedStat in moveUsed.NerfedStats)
                    {
                        defender.ModifiedStats[nerfedStat] -= (int)Math.Round(.5 * defender.ModifiedStats[nerfedStat], MidpointRounding.AwayFromZero);
                    }
                }

            }
            else if (moveUsed.Category == MoveCategory.Physical)
            {
                effectiveness = TypeChart.GetEffectiveness(moveUsed.Typing, BeastBaseDB.BeastBases[defender.Name].Typing1, BeastBaseDB.BeastBases[defender.Name].Typing2);
                damage = (int)Math.Round(moveUsed.Power / 100f * (attacker.ModifiedStats[StatID.Attack] - defender.ModifiedStats[StatID.Defense]) * effectiveness, MidpointRounding.AwayFromZero);
            }
            else if (moveUsed.Category == MoveCategory.Special)
            {
                effectiveness = TypeChart.GetEffectiveness(moveUsed.Typing, BeastBaseDB.BeastBases[defender.Name].Typing1, BeastBaseDB.BeastBases[defender.Name].Typing2);
                damage = (int)Math.Round(moveUsed.Power / 100f * (attacker.ModifiedStats[StatID.SpecialAttack] - defender.ModifiedStats[StatID.SpecialDefense]) * effectiveness, MidpointRounding.AwayFromZero);
            }

            if (moveUsed.Category == MoveCategory.Physical || moveUsed.Category == MoveCategory.Special)
            {

                if (damage <= 0)
                    damage = 1;
            }
            else { damage = 0; }


            defender.ModifiedStats[StatID.HP] -= damage;
        }
        //       }

        
        if(attacker.NewStatusCounter != 0) attacker.NewStatusCounter--;
        if(attacker.confusionCounter != 0) attacker.confusionCounter--;
        statusConditionActivated = false;

        return effectiveness;

    }
    
    public static string FoeString(Beast beast)
    {
        if (beast.IsPlayerUnit)
        {
            return "";
        }
        return "Foe";
    }

}
