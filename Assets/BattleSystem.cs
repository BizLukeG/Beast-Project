using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum BattleState
{
    Battle, BattleOver
}

public class BattleSystem : MonoBehaviour
{
    static public bool isTrainerBattle { set; get; } = false;
    static public bool isWildBattle { set; get; } = false;
    //not making this static gave error saying it needs an object reference when accessing even in same class
    static Queue<MoveID> MovesQueue { set; get; } = new Queue<MoveID>();
    static public BattleState BattleState { get; set; }
    static public Stack<BattleState> BattleStateStack = new Stack<BattleState>();
    static public Beast PlayerActiveBeast { get; set; }
    static public Beast WildBeast { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void HandleBattleGameState()
    {
        

        if (BattleStateStack.Peek() == BattleState.Battle)
        {
            HandleBattleBattleState();
        }else if(BattleStateStack.Peek() == BattleState.BattleOver)
        {
            //maybe have to use GameController.GetComponent<GameController>() to get an instance of GameController
            GameController.GameStateStack.Pop();
            Debug.Log("Does this ever get called??");
        }

        
    }

    public static void HandleBattleBattleState()
    {
        if (isWildBattle)
        {
            //WildBeast = Area.getBeastPerRoute(AreaID.Route101);
            PlayerActiveBeast = Player.Party[0];
            System.Random r = new System.Random();
            Beast firstUnitToMove;
            Beast secondUnitToMove;
            Debug.Log($"WildBeastSpeed {WildBeast.CurrentSpeed} \nplayerActiveBeast {PlayerActiveBeast.CurrentSpeed}");
            

            if (WildBeast.Speed >= PlayerActiveBeast.Speed)
            {
                firstUnitToMove = WildBeast;
                secondUnitToMove = PlayerActiveBeast;

                int rInt = r.Next(0, 4);
                MovesQueue.Enqueue(WildBeast.MoveSet[rInt]);

                int rpInt = r.Next(0, 4);
                MovesQueue.Enqueue(PlayerActiveBeast.MoveSet[rpInt]);
            }
            else
            {
                firstUnitToMove = PlayerActiveBeast;
                secondUnitToMove = WildBeast;

                int rpInt = r.Next(0, 4);
                MovesQueue.Enqueue(PlayerActiveBeast.MoveSet[rpInt]);

                int rInt = r.Next(0, 4);
                MovesQueue.Enqueue(WildBeast.MoveSet[rInt]);
            }
            Debug.Log($"FirstUnitToMove {firstUnitToMove.Name} \nSecondUnitToMove {secondUnitToMove.Name}");

            ExecuteMove(firstUnitToMove, secondUnitToMove);



         
        }
    }

    static void ExecuteMove(Beast firstMover, Beast secondMover)
    {
        
       int damage = (int)Math.Round(MoveDB.Moves[MovesQueue.Dequeue()].Power/100f * (firstMover.CurrentAtt - secondMover.CurrentDef), MidpointRounding.AwayFromZero);
       if (damage <= 0 ){
        damage = 1;
       }

        secondMover.CurrentHP -= damage;
        Debug.Log($"FirstMoverCA {firstMover.CurrentAtt} \n secondMoverCurrentDef {secondMover.CurrentDef} \n damage {damage} \n secondMoverHP {secondMover.CurrentHP}");

        if (IsBattleOver())
        {
            return;
        }

        damage = (int)Math.Round(MoveDB.Moves[MovesQueue.Dequeue()].Power / 100f * (secondMover.CurrentAtt - firstMover.CurrentDef), MidpointRounding.AwayFromZero);
        if (damage <= 0)
        {
            damage = 1;
        }
        
        firstMover.CurrentHP -= damage;
        Debug.Log($"SecondMoverCA {secondMover.CurrentAtt} \n FirstMoverCurrentDef {firstMover.CurrentDef} \n damage {damage} \n firstMoverHP {firstMover.CurrentHP}");

        if (IsBattleOver())
        {
            return;
        }
    }

    static bool IsBattleOver()
    {
        if (WildBeast.CurrentHP <= 0)
        {
            Debug.Log("Player wins ");
            BattleStateStack.Push(BattleState.BattleOver);
            return true;
        }
        else if (PlayerActiveBeast.CurrentHP <= 0)
        {
            Debug.Log("WildBeast wins ");
            BattleStateStack.Push(BattleState.BattleOver);
            return true;
        }
        return false;
    }
}
