using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum BattleState
{
    StartBattle, ActionSelection, MoveSelection, ExecuteMoves, BattleOver
}

public class BattleSystem
{
    static public bool isTrainerBattle { set; get; } = false;
    static public bool isWildBattle { set; get; } = false;
    //not making this static gave error saying it needs an object reference when accessing even in same class
    static Queue<MoveID> MovesQueue { set; get; } = new Queue<MoveID>();
    static public BattleState BattleState { get; set; }
    static public Stack<BattleState> BattleStateStack = new Stack<BattleState>();
    static public Beast PlayerActiveBeast { get; set; }
    static public Beast WildBeast { get; set; }
    public GameObject EnemyBattleUnitUIGO;
    //public BattleUnitUI EnemyBattleUnitUI = new BattleUnitUI();
    //public BattleUnitUI PlayerBattleUnitUI = new BattleUnitUI();
    //public BattleUnitUI BattleUnitUI;
    public int NoMatter;

    // Awake is called when Scene loads only works when inheriting from monoBehaviour
    //void Awake()
    //{
    //    EnemyBattleUnitUI = new BattleUnitUI();
    //    EnemyBattleUnitUIGO = GameObject.Find("EnemyUnitUI");
    //    EnemyBattleUnitUI = EnemyBattleUnitUIGO.GetComponent<BattleUnitUI>(); 
    //}

   

    static public void HandleGameStateBattle()
    {
        //EnemyBattleUnitUI = new BattleUnitUI();
        //Debug.Log("BattleUnitUI " + EnemyBattleUnitUI.Hi);

        if (BattleStateStack.Peek() == BattleState.StartBattle)
        {
            HandleBattleStateStartBattle();
        }else if (BattleStateStack.Peek() == BattleState.ActionSelection)
        {
            
            ActionSelector.HandleBattleStateActionSelection();
        }else if (BattleStateStack.Peek() == BattleState.MoveSelection)
        {

            MoveSelector.HandleBattleStateMoveSelection();
        }
        else if (BattleStateStack.Peek() == BattleState.ExecuteMoves)
        {

            ExecuteMoves();
        }
        else if(BattleStateStack.Peek() == BattleState.BattleOver)
        {
            //maybe have to use GameController.GetComponent<GameController>() to get an instance of GameController
            GameController.GameStateStack.Pop();
            Debug.Log("Does this ever get called??");
        }

        
    }

    static public void HandleBattleStateStartBattle()
    {
        if (isWildBattle)
        {
            //WildBeast = Area.getBeastPerRoute(AreaID.Route101);
            PlayerActiveBeast = Player.Party[0];

            
            BattleUnitUI.SetupEnemy(WildBeast);
            BattleUnitUI.SetupPlayer(PlayerActiveBeast);

            BattleStateStack.Push(BattleState.ActionSelection);
            

         
        }
    }

    static void ExecuteMoves()
    {
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

            MovesQueue.Enqueue(MoveSelector.SelectedMove);
        }
        else
        {
            firstUnitToMove = PlayerActiveBeast;
            secondUnitToMove = WildBeast;


            MovesQueue.Enqueue(MoveSelector.SelectedMove);

            int rInt = r.Next(0, 4);
            MovesQueue.Enqueue(WildBeast.MoveSet[rInt]);
        }
        Debug.Log($"FirstUnitToMove {firstUnitToMove.Name} \nSecondUnitToMove {secondUnitToMove.Name}");

        int damage = (int)Math.Round(MoveDB.Moves[MovesQueue.Dequeue()].Power/100f * (firstUnitToMove.CurrentAtt - secondUnitToMove.CurrentDef), MidpointRounding.AwayFromZero);
       if (damage <= 0 ){
        damage = 1;
       }

        secondUnitToMove.CurrentHP -= damage;
        Debug.Log($"FirstMoverCA {firstUnitToMove.CurrentAtt} \n secondMoverCurrentDef {secondUnitToMove.CurrentDef} \n damage {damage} \n secondMoverHP {secondUnitToMove.CurrentHP}");

        BattleUnitUI.SetupEnemy(WildBeast);
        BattleUnitUI.SetupPlayer(PlayerActiveBeast);

        if (IsBattleOver())
        {
            BattleStateStack.Push(BattleState.BattleOver);
            HandleGameStateBattle();
        }

        damage = (int)Math.Round(MoveDB.Moves[MovesQueue.Dequeue()].Power / 100f * (secondUnitToMove.CurrentAtt - firstUnitToMove.CurrentDef), MidpointRounding.AwayFromZero);
        if (damage <= 0)
        {
            damage = 1;
        }

        firstUnitToMove.CurrentHP -= damage;
        Debug.Log($"SecondMoverCA {secondUnitToMove.CurrentAtt} \n FirstMoverCurrentDef {firstUnitToMove.CurrentDef} \n damage {damage} \n firstMoverHP {firstUnitToMove.CurrentHP}");

        BattleUnitUI.SetupEnemy(WildBeast);
        BattleUnitUI.SetupPlayer(PlayerActiveBeast);

        if (IsBattleOver())
        {
            BattleStateStack.Push(BattleState.BattleOver);
            HandleGameStateBattle();
        }

        Debug.Log("BSS Count " + BattleStateStack.Count);


        while (BattleStateStack.Count > 2)
        {
            
            BattleStateStack.Pop();
            Debug.Log("BSS Count in While " + BattleStateStack.Count);
        }
            
    }

    static bool IsBattleOver()
    {
        if (WildBeast.CurrentHP <= 0)
        {
            Debug.Log("Player wins ");          
            return true;
        }
        else if (PlayerActiveBeast.CurrentHP <= 0)
        {
            Debug.Log("WildBeast wins ");
            return true;
        }
        return false;
    }
}
