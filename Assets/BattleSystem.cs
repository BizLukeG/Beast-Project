using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum BattleState
{
    StartBattle, Typing, FinishDialog, ActionSelection, MoveSelection, ExecuteMoves, Dialog, BattleOver
}

public class BattleSystem : MonoBehaviour
{
    static public bool isTrainerBattle { set; get; } = false;
    public bool isWildBattle { set; get; } = false;
    //not making this static gave error saying it needs an object reference when accessing even in same class
    static Queue<MoveID> MovesQueue { set; get; } = new Queue<MoveID>();
    static public BattleState BattleState { get; set; }
    public Stack<BattleState> BattleStateStack = new Stack<BattleState>();
    static public Beast PlayerActiveBeast { get; set; }
    public Beast WildBeast { get; set; }
    static public GameObject ActionSelectorGO { get; set; }
    static public GameObject MoveSelectorGO { get; set; }
    //static bool hold = true;
    BattleDialogBox BattleDialogBoxMB;
    public ActionSelector ActionSelectorMB;
    public MoveSelector MoveSelectorMB;

    //public GameObject EnemyBattleUnitUIGO;
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

    void Awake()
    {
        BattleDialogBoxMB = GameObject.Find("Battle DialogBox").GetComponent<BattleDialogBox>();
        ActionSelectorMB = GameObject.Find("Battle Action Selector").GetComponent<ActionSelector>();
        MoveSelectorMB = GameObject.Find("Battle Move Selector").GetComponent<MoveSelector>();
    }

    public void HandleGameStateBattle()
    {
        //EnemyBattleUnitUI = new BattleUnitUI();
        //Debug.Log("BattleUnitUI " + EnemyBattleUnitUI.Hi);

        if (BattleStateStack.Peek() == BattleState.StartBattle)
        {
            //can't use Coroutine on a static anything
            //SetUpBattle();
            StartCoroutine(HandleBattleStateStartBattle());
            //HandleBattleStateStartBattle();
        }
        else if (BattleStateStack.Peek() == BattleState.ActionSelection)
        {
            
            ActionSelectorMB.gameObject.SetActive(true);
            ActionSelectorMB.HandleBattleStateActionSelection();
        }else if (BattleStateStack.Peek() == BattleState.MoveSelection)
        {
            MoveSelectorMB.gameObject.SetActive(true);
            MoveSelectorMB.SetMoveNames(PlayerActiveBeast.MoveSet);
            MoveSelectorMB.HandleBattleStateMoveSelection();
        }
        else if (BattleStateStack.Peek() == BattleState.ExecuteMoves)
        {

            ExecuteMoves();
        }
        else if(BattleStateStack.Peek() == BattleState.BattleOver)
        {
            
            BattleStateStack.Clear();
                

            //maybe have to use GameController.GetComponent<GameController>() to get an instance of GameController
            GameController.GameStateStack.Pop();
            Debug.Log("Does this ever get called??");
        }
        else if (BattleStateStack.Peek() == BattleState.FinishDialog)
        {

            if (Input.GetKeyDown(KeyCode.X))
            {
                BattleStateStack.Push(BattleState.ActionSelection);
            }
        }


    }

    //public void SetUpBattle()
    //{
    //    StartCoroutine(HandleBattleStateStartBattle());
    //}

    //can't use Coroutine on a static anything
    public IEnumerator HandleBattleStateStartBattle()
    {
        BattleStateStack.Push(BattleState.Typing);

        if (isWildBattle)
        {
            //WildBeast = Area.getBeastPerRoute(AreaID.Route101);
            PlayerActiveBeast = Player.Party[0];

            
            BattleUnitUI.SetupEnemy(WildBeast);
            BattleUnitUI.SetupPlayer(PlayerActiveBeast);

            yield return BattleDialogBoxMB.DisplayBattleDialogText("A Wild Beast Appeared");
            //yield return new WaitUntil(() => BattleDialogBoxMB.IsTyping == false);

            BattleStateStack.Push(BattleState.FinishDialog);

            //if (Input.GetKeyDown(KeyCode.X))
            //{
            //    BattleStateStack.Push(BattleState.ActionSelection);
            //}
            

            //BattleStateStack.Push(BattleState.ActionSelection);
            
        }

    }



    void ExecuteMoves()
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
            //HandleGameStateBattle();
        }
        else
        {
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

            }
        }

       

        Debug.Log("BSS Count " + BattleStateStack.Count);


        if (BattleStateStack.Peek() != BattleState.BattleOver)
        {
            BattleStateStack.Clear();
            BattleStateStack.Push(BattleState.ActionSelection);
            Debug.Log("BSS Count in While " + BattleStateStack.Count);
        }

    }

    bool IsBattleOver()
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
