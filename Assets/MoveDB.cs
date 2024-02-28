using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveID
{
    Smack, Pound, Tackle, LeafStorm, FlameWheel, RockSlide
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

        {MoveID.Smack, new Move(){ Power = 20, Accuracy = 100, Typing = Typing.Native}},
        {MoveID.Pound, new Move(){ Power = 30, Accuracy = 100, Typing = Typing.Native}},
        {MoveID.Tackle, new Move(){ Power = 40, Accuracy = 100, Typing = Typing.Native}},
        {MoveID.LeafStorm, new Move(){ Power = 50, Accuracy = 100, Typing = Typing.Nature}},
        {MoveID.FlameWheel, new Move(){ Power = 60, Accuracy = 100, Typing = Typing.Sacred}},
        {MoveID.RockSlide, new Move(){ Power = 70, Accuracy = 100, Typing = Typing.Rock}},
       
    };
    
}
