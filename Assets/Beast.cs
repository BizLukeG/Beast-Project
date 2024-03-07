using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

        
        if (attacker.Condition != ConditionID.None)
        {
            //Debug.Log("DamageCalc1");
            IsBeforeMoveActivated = ConditionDB.Conditions[attacker.Condition].OnBeforeMove(attacker);
        }


        //Debug.Log("confusedNum2 " + confusedNum);
        if (!IsBeforeMoveActivated)
        {
            BattleDialog.Enqueue($"{FoeString(attacker)} {attacker.Name} used {moveUsed.Name}");

        }else if (attacker.Condition == ConditionID.Confused){

                BattleDialog.Enqueue($"{FoeString(attacker)} {attacker.Name} {ConditionDB.Conditions[attacker.Condition].FullyConfusedMessage}");

                effectiveness = TypeChart.GetEffectiveness(moveUsed.Typing, BeastBaseDB.BeastBases[defender.Name].Typing1, BeastBaseDB.BeastBases[defender.Name].Typing2);

                if (moveUsed.Category == MoveCategory.Physical)
                {
                    damage = (int)Math.Round(moveUsed.Power / 100f * (attacker.ModifiedStats[StatID.Attack] - attacker.ModifiedStats[StatID.Defense]) * effectiveness, MidpointRounding.AwayFromZero);
                }
                else
                {

                    damage = (int)Math.Round(moveUsed.Power / 100f * (attacker.ModifiedStats[StatID.SpecialAttack] - attacker.ModifiedStats[StatID.SpecialDefense]) * effectiveness, MidpointRounding.AwayFromZero);
                    //Debug.Log($"confusion damage breakdown: Power {moveUsed.Power}, SpA: {attacker.ModifiedStats[StatID.SpecialAttack]}, SpD ");
                }

                Debug.Log("confusion damage " + damage);

                if (damage <= 0)
                {
                    damage = 1;
                }

                attacker.ModifiedStats[StatID.HP] -= damage;
        }
                

        if (moveUsed.Category == MoveCategory.Status)
        {
            if (defender.Status == StatusID.None)
            {
                defender.Status = moveUsed.Status;
                StatusDB.Statuses[defender.Status].OnStatusActivated(defender);
                BattleDialog.Enqueue($"{FoeString(defender)} {defender.Name} {StatusDB.Statuses[defender.Status].ActivationMessage}");
            }
            else
            {
                BattleDialog.Enqueue($"{FoeString(defender)} {defender.Name} is already {defender.Status.ToString()}");
            }
        }else if(moveUsed.Category == MoveCategory.Condition)
        {
            if( defender.Condition != moveUsed.Condition)
            {
                defender.Condition = moveUsed.Condition;
                ConditionDB.Conditions[defender.Condition].OnConditionActivated(defender);
                BattleDialog.Enqueue($"{FoeString(defender)} {defender.Name} {ConditionDB.Conditions[defender.Condition].ActivationMessage}");
            }
            else
            {
                BattleDialog.Enqueue($"{FoeString(defender)} {defender.Name} is already {defender.Condition.ToString()}");
            }
            
        }

        else if (moveUsed.Category == MoveCategory.ModifyStats)
        {
            

            if(moveUsed.TargetSelf == true)
            {
               
                foreach (var buffedStat in moveUsed.BuffedStats)
                {
                    attacker.ModifiedStats[buffedStat] += (int)Math.Round(.5 * attacker.ModifiedStats[buffedStat], MidpointRounding.AwayFromZero);
                }
                foreach (var nerfedStat in moveUsed.NerfedStats)
                {
                    attacker.ModifiedStats[nerfedStat] -= (int)Math.Round(.5 * attacker.ModifiedStats[nerfedStat], MidpointRounding.AwayFromZero);
                }
                    
            }else if(moveUsed.TargetSelf == false)
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
                     
        } else if (moveUsed.Category == MoveCategory.Physical)
        {
            effectiveness = TypeChart.GetEffectiveness(moveUsed.Typing, BeastBaseDB.BeastBases[defender.Name].Typing1, BeastBaseDB.BeastBases[defender.Name].Typing2);
            damage = (int)Math.Round(moveUsed.Power / 100f * (attacker.ModifiedStats[StatID.Attack] - defender.ModifiedStats[StatID.Defense]) * effectiveness, MidpointRounding.AwayFromZero);
        }
        else if (moveUsed.Category == MoveCategory.Special)
        {
            effectiveness = TypeChart.GetEffectiveness(moveUsed.Typing, BeastBaseDB.BeastBases[defender.Name].Typing1, BeastBaseDB.BeastBases[defender.Name].Typing2);
            damage = (int)Math.Round(moveUsed.Power / 100f * (attacker.ModifiedStats[StatID.SpecialAttack] - defender.ModifiedStats[StatID.SpecialDefense]) * effectiveness, MidpointRounding.AwayFromZero);
        }

        if (moveUsed.Category == MoveCategory.Physical || moveUsed.Category == MoveCategory.Special) {
            
            if (damage <= 0)
            damage = 1;
        }
        else { damage = 0; }

        if (!IsBeforeMoveActivated)
        {
            Debug.Log("DamageCalc3");
            defender.ModifiedStats[StatID.HP] -= damage;
        }
        

        

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
