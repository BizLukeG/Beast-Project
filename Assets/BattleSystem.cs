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
    public Queue<string> BattleDialog { set; get; } = new Queue<string>();


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
            
            BattleStateStack.Clear();
                

            //maybe have to use GameController.GetComponent<GameController>() to get an instance of GameController
            GameController.GameStateStack.Pop();
            Debug.Log("Does this ever get called??");
        }
        //else if (BattleStateStack.Peek() == BattleState.FinishDialog)
        //{

        //    if (Input.GetKeyDown(KeyCode.X))
        //    {
        //        BattleStateStack.Push(BattleState.ActionSelection);
        //    }
        //}
        else if (BattleStateStack.Peek() == BattleState.Hold)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                hold = false;
                
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

            yield return BattleDialogBoxMB.DisplayBattleDialogText("A Wild Beast Appeared. Press X To Continue");
            //yield return new WaitUntil(() => BattleDialogBoxMB.IsTyping == false);

            BattleStateStack.Push(BattleState.Hold);

            yield return new WaitUntil(() => hold == false);
            hold = true;

            BattleStateStack.Push(BattleState.ActionSelection);
            //if (Input.GetKeyDown(KeyCode.X))
            //{
            //    BattleStateStack.Push(BattleState.ActionSelection);
            //}


            //BattleStateStack.Push(BattleState.ActionSelection);

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
        
        //float effectiveness = TypeChart.GetEffectiveness(moveUsed.Typing, BeastBaseDB.BeastBases[secondUnitToMove.Name].Typing1, BeastBaseDB.BeastBases[secondUnitToMove.Name].Typing2);
        float effectiveness = Beast.DamageCalc(moveUsed, firstUnitToMove, secondUnitToMove, true);
        string effectivenessPhrase = TypeChart.GetEffectivenessPhrase(effectiveness);

        // int damage = (int)Math.Round(moveUsed.Power/100f * (firstUnitToMove.CurrentAtt - secondUnitToMove.CurrentDef) * effectiveness, MidpointRounding.AwayFromZero);
        //if (damage <= 0 ){
        // damage = 1;
        //}

        //while (BattleDialog.Count > 0)
        //{
            //yield return BattleDialogBoxMB.DisplayBattleDialogText($"{BattleDialog.Dequeue}");
        //}


        //applies damage
        //secondUnitToMove.CurrentHP -= damage;
        //Debug.Log($"FirstMoverCA {firstUnitToMove.CurrentAtt} \n secondMoverCurrentDef {secondUnitToMove.CurrentDef} \n damage {damage} \n secondMoverHP {secondUnitToMove.CurrentHP}");

        //displays move used
        //goes back to update with runturn now the battlestate so nothing happens until this is over and then continues from here
        if (!firstUnitToMove.IsPlayerUnit)
        {
            yield return BattleDialogBoxMB.DisplayBattleDialogText($"Foe {firstUnitToMove.Name} used {moveUsed.Name}");
        }
        else
        {
            yield return BattleDialogBoxMB.DisplayBattleDialogText($"{firstUnitToMove.Name} used {moveUsed.Name}");
        }

        //waits for x to be pressed to continue
        //BattleStateStack.Push(BattleState.Hold);
        //yield return new WaitUntil(() => hold == false);
        //BattleStateStack.Pop();
        //hold = true;

        yield return new WaitForSeconds(1.5f);

        //displays stat change
        BattleUnitUI.SetupEnemy(WildBeast);
        BattleUnitUI.SetupPlayer(PlayerActiveBeast);

        yield return new WaitForSeconds(1f);

        if (effectiveness != 1)
        {
            yield return BattleDialogBoxMB.DisplayBattleDialogText($"{effectivenessPhrase}");
            yield return new WaitForSeconds(1.5f);
        }

        

        if (IsBattleOver())
        {
            yield return BattleDialogBoxMB.DisplayBattleDialogText("Battle is over. Press X To Continue");
            BattleStateStack.Push(BattleState.Hold);
            yield return new WaitUntil(() => hold == false);
            BattleStateStack.Pop();
            hold = true;
            PlayerActiveBeast.ResetStats();
            BattleStateStack.Push(BattleState.BattleOver);
            //yield return BattleDialogBoxMB.DisplayBattleDialogText("Battle is over");
            //HandleGameStateBattle();
        }
        else
        {
            moveUsed = MoveDB.Moves[MovesQueue.Dequeue()];

            //effectiveness = TypeChart.GetEffectiveness(moveUsed.Typing, BeastBaseDB.BeastBases[firstUnitToMove.Name].Typing1, BeastBaseDB.BeastBases[firstUnitToMove.Name].Typing2);
            effectiveness = Beast.DamageCalc(moveUsed, firstUnitToMove, secondUnitToMove, false);
            effectivenessPhrase = TypeChart.GetEffectivenessPhrase(effectiveness);

            //damage = (int)Math.Round(moveUsed.Power / 100f * (secondUnitToMove.CurrentAtt - firstUnitToMove.CurrentDef) * effectiveness, MidpointRounding.AwayFromZero);
            //if (damage <= 0)
            //{
            //    damage = 1;
            //}

            //firstUnitToMove.CurrentHP -= damage;

            //Debug.Log($"SecondMoverCA {secondUnitToMove.CurrentAtt} \n FirstMoverCurrentDef {firstUnitToMove.CurrentDef} \n damage {damage} \n firstMoverHP {firstUnitToMove.CurrentHP}");
            if (!secondUnitToMove.IsPlayerUnit)
            {
                yield return BattleDialogBoxMB.DisplayBattleDialogText($"Foe {secondUnitToMove.Name} used {moveUsed.Name}");
            }else
            {
                yield return BattleDialogBoxMB.DisplayBattleDialogText($"{secondUnitToMove.Name} used {moveUsed.Name}");
            }
            
            //BattleStateStack.Push(BattleState.Hold);
            //yield return new WaitUntil(() => hold == false);
            //BattleStateStack.Pop();
            //hold = true;
            yield return new WaitForSeconds(1.5f);

            BattleUnitUI.SetupEnemy(WildBeast);
            BattleUnitUI.SetupPlayer(PlayerActiveBeast);

            yield return new WaitForSeconds(1f);

            if (effectiveness != 1)
            {
                yield return BattleDialogBoxMB.DisplayBattleDialogText($"{effectivenessPhrase}");
                yield return new WaitForSeconds(1.5f);
            }


            if (IsBattleOver())
            {
                yield return BattleDialogBoxMB.DisplayBattleDialogText("Battle is over. Press X To Continue");
                BattleStateStack.Push(BattleState.Hold);
                yield return new WaitUntil(() => hold == false);
                BattleStateStack.Pop();
                hold = true;
                PlayerActiveBeast.ResetStats();
                BattleStateStack.Push(BattleState.BattleOver);

                //yield return BattleDialogBoxMB.DisplayBattleDialogText("Battle is over");
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
