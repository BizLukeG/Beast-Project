using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveID
{
    Smack, Pound, Tackle, LeafStorm, FlameWheel, RockSlide, Harden, Agility, Scorch, ConfuseRay, ThunderWave, Bite, SleepPowder, Attract, Freeze, FlameBurst, Psybeam, PoisonSting, Pollute
}

public enum MoveCategory
{
    Physical, Special, Status, ModifyStats, Condition
}

//need a statusDB class
public enum StatusID
{
    None, Burned, Poisoned, /*p1beforeturn*/Asleep, /*p5beforeturn*/Paralyzed, /*p1beforeturn*/Frozen
}

//add all to an arraylist. get eachs priority. Top Priorties message gets read the rest get skipped. If lower prority than Confusion or Enamored still read their initial messages

public enum ConditionID
{
    None, /*p3beforeturn*/Confused, /*p2beforeturn*/Flinched, /*p4beforeturn*/Enamored
}

public class MoveDB
{

    public static void Init()
    {
        foreach (var kvp in Moves)
        {
            var moveName = kvp.Key;
            var move = kvp.Value;

            move.Name = moveName;
        }
    }

    public static Dictionary<MoveID, Move> Moves { get; set; } = new Dictionary<MoveID, Move>()
    {

        {MoveID.Smack, new Move(){ Power = 20, Accuracy = 100, Typing = Typing.Native, Category = MoveCategory.Physical}},
        {MoveID.Pound, new Move(){ Power = 30, Accuracy = 100, Typing = Typing.Native, Category = MoveCategory.Physical}},
        {MoveID.Tackle, new Move(){ Power = 40, Accuracy = 100, Typing = Typing.Native, Category = MoveCategory.Physical}},
        {MoveID.LeafStorm, new Move(){ Power = 50, Accuracy = 100, Typing = Typing.Nature, Category = MoveCategory.Special}},
        {MoveID.FlameWheel, new Move(){ Power = 60, Accuracy = 100, Typing = Typing.Sacred, Category = MoveCategory.Physical}},
        {MoveID.RockSlide, new Move(){ Power = 70, Accuracy = 100, Typing = Typing.Rock, Category = MoveCategory.Physical}},
        {MoveID.Harden, new Move(){ Power = 0, Accuracy = 100, Typing = Typing.Rock, Category = MoveCategory.ModifyStats, BuffedStats = new List<StatID>{StatID.Attack}, NerfedStats = new List<StatID>{StatID.Defense}, TargetSelf = true}},
        {MoveID.Agility, new Move(){ Power = 0, Accuracy = 100, Typing = Typing.Rock, Category = MoveCategory.ModifyStats, BuffedStats = new List<StatID>{StatID.Speed}, TargetSelf = true}},
        {MoveID.Scorch, new Move(){ Power = 0, Accuracy = 50, Typing = Typing.Sacred, Category = MoveCategory.Status, Status = StatusID.Burned}},
        {MoveID.ConfuseRay, new Move(){ Power = 0, Accuracy = 100, Typing = Typing.Native, Category = MoveCategory.Condition, Condition = ConditionID.Confused}},
        {MoveID.ThunderWave, new Move(){ Power = 0, Accuracy = 100, Typing = Typing.Static, Category = MoveCategory.Status, Status = StatusID.Paralyzed}},
        {MoveID.Bite, new Move(){ Power = 50, Accuracy = 50, Typing = Typing.Corrupt, Category = MoveCategory.Physical, SecondaryEffectCategory = MoveCategory.Condition, SecondaryEffectCondition = ConditionID.Flinched, SecondaryEffectChance = 50}},
        {MoveID.SleepPowder, new Move(){ Power = 0, Accuracy = 100, Typing = Typing.Nature, Category = MoveCategory.Status, Status = StatusID.Asleep}},
        {MoveID.Attract, new Move(){ Power = 0, Accuracy = 100, Typing = Typing.Nature, Category = MoveCategory.Condition, Condition = ConditionID.Enamored}},
        {MoveID.Freeze, new Move(){ Power = 0, Accuracy = 100, Typing = Typing.Nature, Category = MoveCategory.Status, Status = StatusID.Frozen}},
        {MoveID.FlameBurst, new Move(){ Power = 60, Accuracy = 100, Typing = Typing.Sacred, Category = MoveCategory.Physical, SecondaryEffectCategory = MoveCategory.Status, SecondaryEffectStatus = StatusID.Burned, SecondaryEffectChance = 50}},
        {MoveID.Psybeam, new Move(){ Power = 60, Accuracy = 100, Typing = Typing.Native, Category = MoveCategory.Special, SecondaryEffectCategory = MoveCategory.Condition, SecondaryEffectCondition = ConditionID.Confused, SecondaryEffectChance = 50}},
        {MoveID.PoisonSting, new Move(){ Power = 60, Accuracy = 100, Typing = Typing.Toxic, Category = MoveCategory.Special, SecondaryEffectCategory = MoveCategory.Status, SecondaryEffectStatus = StatusID.Poisoned, SecondaryEffectChance = 50}},
        {MoveID.Pollute, new Move(){ Power = 0, Accuracy = 50, Typing = Typing.Toxic, Category = MoveCategory.Status, Status = StatusID.Poisoned}},
    };
    
}
