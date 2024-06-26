using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum BattleState
{
    StartBattle, Typing, RunMoves, Hold, FinishDialog, ActionSelection, MoveSelection, ExecuteMoves, Dialog, BattleOver
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
    bool hold = true;
    public BattleDialogBox BattleDialogBoxMB;
    public ActionSelector ActionSelectorMB;
    public MoveSelector MoveSelectorMB;
    //public Queue<string> BattleDialog { set; get; } = new Queue<string>();


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
        

        if (BattleStateStack.Peek() == BattleState.StartBattle)
        {
            Debug.Log("StartBattle");
            //can't use Coroutine on a static anything

            StartCoroutine(HandleBattleStateStartBattle());
  
        }
        else if (BattleStateStack.Peek() == BattleState.ActionSelection)
        {
            Debug.Log("ActionSelection");
            ActionSelectorMB.gameObject.SetActive(true);
            BattleDialogBoxMB.DisplayBattleDialogTextNoAnimation("What will you do?");
            ActionSelectorMB.HandleBattleStateActionSelection();
        }else if (BattleStateStack.Peek() == BattleState.MoveSelection)
        {
            MoveSelectorMB.gameObject.SetActive(true);
            MoveSelectorMB.SetMoveNames(PlayerActiveBeast.MoveSet);
            MoveSelectorMB.HandleBattleStateMoveSelection();
        }
        else if (BattleStateStack.Peek() == BattleState.ExecuteMoves)
        {

            StartCoroutine(ExecuteMoves());
        }
        else if(BattleStateStack.Peek() == BattleState.BattleOver)
        {
            
            //BattleStateStack.Clear();
                

            GameController.GameStateStack.Pop();
            Debug.Log("Does this ever get called??");
        }
        else if (BattleStateStack.Peek() == BattleState.Hold)
        {
            Debug.Log("Holding");
            if (Input.GetKeyDown(KeyCode.X))
            {
                hold = false;
                
            }
            
        }


    }


    public IEnumerator HandleBattleStateStartBattle()
    {
        BattleStateStack.Push(BattleState.Typing);

        if (isWildBattle)
        {
            //WildBeast = Area.getBeastPerRoute(AreaID.Route101);
            PlayerActiveBeast = Player.Party[0];

            
            BattleUnitUI.SetupEnemy(WildBeast);
            BattleUnitUI.SetupPlayer(PlayerActiveBeast);

            yield return BattleDialogBoxMB.DisplayBattleDialogText("A Wild Beast Appeared. Press X To Continue");
  

            Debug.Log("Battle wait for X");

            BattleStateStack.Push(BattleState.Hold);

            yield return new WaitUntil(() => hold == false);
            hold = true;

            BattleStateStack.Push(BattleState.ActionSelection);


        }

    }



    IEnumerator ExecuteMoves()
    {
        BattleStateStack.Push(BattleState.RunMoves);
        ActionSelectorMB.gameObject.SetActive(false);
        MoveSelectorMB.gameObject.SetActive(false);
        System.Random r = new System.Random();
        Beast firstUnitToMove;
        Beast secondUnitToMove;
        Debug.Log($"WildBeastSpeed {WildBeast.ModifiedStats[StatID.Speed]} \nplayerActiveBeast {PlayerActiveBeast.ModifiedStats[StatID.Speed]}");
        
        //Calcs who goes first
        if (WildBeast.ModifiedStats[StatID.Speed] >= PlayerActiveBeast.ModifiedStats[StatID.Speed])
        {
            firstUnitToMove = WildBeast;
            secondUnitToMove = PlayerActiveBeast;

            int rInt = r.Next(0, 4);
            MovesQueue.Enqueue(WildBeast.MoveSet[0/*rInt*/]);

            MovesQueue.Enqueue(MoveSelectorMB.SelectedMove);
        }
        else
        {
            firstUnitToMove = PlayerActiveBeast;
            secondUnitToMove = WildBeast;


            MovesQueue.Enqueue(MoveSelectorMB.SelectedMove);

            int rInt = r.Next(0, 4);
            MovesQueue.Enqueue(WildBeast.MoveSet[0/*rInt*/]);
        }
        Debug.Log($"FirstUnitToMove {firstUnitToMove.Name} \nSecondUnitToMove {secondUnitToMove.Name}");

        //Calcs damage
        Move moveUsed = MoveDB.Moves[MovesQueue.Dequeue()];
        

        float effectiveness = Beast.DamageCalc(moveUsed, firstUnitToMove, secondUnitToMove, true);
        string effectivenessPhrase = TypeChart.GetEffectivenessPhrase(effectiveness);





        while (Beast.BattleDialog.Count > 0)
        {
            yield return BattleDialogBoxMB.DisplayBattleDialogText(Beast.BattleDialog.Dequeue());
            yield return new WaitForSeconds(1.5f);
        }

        //displays stat change
        BattleUnitUI.SetupEnemy(WildBeast);
        BattleUnitUI.SetupPlayer(PlayerActiveBeast);

        yield return new WaitForSeconds(1f);



        if (IsBattleOver())
        {
            yield return BattleDialogBoxMB.DisplayBattleDialogText("Battle is over. Press X To Continue");
            BattleStateStack.Push(BattleState.Hold);
            yield return new WaitUntil(() => hold == false);
            BattleStateStack.Pop();
            hold = true;
            PlayerActiveBeast.ResetStats();
            BattleStateStack.Push(BattleState.BattleOver);

        }
        else
        {
            moveUsed = MoveDB.Moves[MovesQueue.Dequeue()];

 
            effectiveness = Beast.DamageCalc(moveUsed, firstUnitToMove, secondUnitToMove, false);
            effectivenessPhrase = TypeChart.GetEffectivenessPhrase(effectiveness);



            while (Beast.BattleDialog.Count > 0)
            {
                yield return BattleDialogBoxMB.DisplayBattleDialogText(Beast.BattleDialog.Dequeue());
                yield return new WaitForSeconds(1.5f);
            }

            BattleUnitUI.SetupEnemy(WildBeast);
            BattleUnitUI.SetupPlayer(PlayerActiveBeast);

            yield return new WaitForSeconds(1f);




            if (IsBattleOver())
            {
                yield return BattleDialogBoxMB.DisplayBattleDialogText("Battle is over. Press X To Continue");
                BattleStateStack.Push(BattleState.Hold);
                yield return new WaitUntil(() => hold == false);
                BattleStateStack.Pop();
                hold = true;
                PlayerActiveBeast.ResetStats();
                BattleStateStack.Push(BattleState.BattleOver);

            }
        }

        Debug.Log("While BattleSystem Count " + Beast.BattleDialog.Count);
        if (!IsBattleOver())
        {
            Beast.DamageCalcAfterTurn(WildBeast, PlayerActiveBeast);
            while (Beast.BattleDialog.Count > 0)
            {
                Debug.Log("While BattleDialog ");
                yield return BattleDialogBoxMB.DisplayBattleDialogText(Beast.BattleDialog.Dequeue());
                yield return new WaitForSeconds(1.5f);
            }
            BattleUnitUI.SetupEnemy(WildBeast);
            if (IsBattleOver())
            {
                yield return BattleDialogBoxMB.DisplayBattleDialogText("Battle is over. Press X To Continue");
                BattleStateStack.Push(BattleState.Hold);
                yield return new WaitUntil(() => hold == false);
                BattleStateStack.Pop();
                hold = true;
                PlayerActiveBeast.ResetStats();
                BattleStateStack.Push(BattleState.BattleOver);

            }
        }

        Debug.Log("BSS Count " + BattleStateStack.Count);


        if (BattleStateStack.Peek() != BattleState.BattleOver)
        {
            MovesQueue.Clear();
            BattleStateStack.Clear();
            BattleStateStack.Push(BattleState.ActionSelection);
            Debug.Log("BSS Count in While " + BattleStateStack.Count);
        }

    }

    bool IsBattleOver()
    {
        if (WildBeast.ModifiedStats[StatID.HP] <= 0)
        {
            WildBeast.AfterTurnDamage = 0;
            Debug.Log("Player wins ");          
            return true;
        }
        else if (PlayerActiveBeast.ModifiedStats[StatID.HP] <= 0)
        {
            Debug.Log("WildBeast wins ");
            return true;
        }
        return false;
    }

    
}
