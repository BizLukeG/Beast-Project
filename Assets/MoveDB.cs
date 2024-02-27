using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveID
{
    smack, pound, tackle
}

public class MoveDB : MonoBehaviour
{
      
    public static Dictionary<MoveID, Move> Moves { get; set; } = new Dictionary<MoveID, Move>()
    {

        {MoveID.smack,
            new Move()
            {
                power = 10,
                
            }
        }
    };
    
}
