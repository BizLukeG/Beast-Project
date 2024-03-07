using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveID
{
    Smack, Pound, Tackle, LeafStorm, FlameWheel, RockSlide, Harden, Agility, Scorch
}

public enum MoveCategory
{
    Physical, Special, Status, ModifyStats
}

//need a statusDB class
public enum StatusID
{
    None, Burned, Poisoned, Asleep, Paralyzed, Frozen, Flinched
}

public enum ConditionID
{
    None, Confused
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
        {MoveID.Scorch, new Move(){ Power = 0, Accuracy = 100, Typing = Typing.Sacred, Category = MoveCategory.Status, Status = StatusID.Burned}},
    };
    
}
