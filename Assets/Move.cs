using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    public int Power { get; set; }
    public int Accuracy { get; set; }
    public Typing Typing { get; set; }
    public MoveID Name { get; set; }
    public MoveCategory Category { get; set; }
    //public StatChange StatChange { get; set; }
    // public StatModID StatMod { get; set; }
    public List<StatID> BuffedStats { get; set; } = new List<StatID>();  
    public List<StatID> NerfedStats { get; set; } = new List<StatID>();
    //public Buff BuffStat { get; set; }
    //public Nerf NerfStat { get; set; }
    public bool TargetSelf {get; set;}
    public StatusID Status { get; set; }
    public ConditionID Condition { get; set; }
    public int StatusPriority { get; set; }
    public StatusID SecondaryEffectStatus { get; set; }
    public ConditionID SecondaryEffectCondition { get; set; }
    public MoveCategory SecondaryEffectCategory { get; set; }
    public int SecondaryEffectChance { get; set; }
    public double HealPercent { get; set; }

    public Move()
    {

    }

}

//Instantiate all moves at beginning of game and put them in a DataBase
//put them in a DataBase, and then instantiate the DataBase