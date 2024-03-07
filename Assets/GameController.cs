using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameState
{
    OverWorld, Battle
}

//MonoBehavior lets u attach a script to a game object that makes and instance of it in the UI also making it so that you can access Instance variables. (I think)
public class GameController : MonoBehaviour
{
    GameState State;
    static public Stack<GameState> GameStateStack { get; set; } = new Stack<GameState>();
    public GameObject BattleSystemGO;
    public BattleSystem BattleSystemMB;

    //public static GameController Instance { get; private set; }

    //public BattleSystem BattleSystem;

    // Awake is called when Scene loads only works when inheriting from monoBehaviour
    void Awake()
    {
        //BattleSystem.ActionSelectorGO = GameObject.Find("Battle Action Selector");
        //BattleSystem.ActionSelectorGO.SetActive(false);
        //BattleSystem.MoveSelectorGO = GameObject.Find("Battle Move Selector");
        //BattleSystem.MoveSelectorGO.SetActive(false);

        //BattleSystem = new BattleSystem();
        //BattleSystemGO = GameObject.Find("BattleSystem");
        BattleSystemMB = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        BattleSystemMB.ActionSelectorMB.gameObject.SetActive(false);
        BattleSystemMB.MoveSelectorMB.gameObject.SetActive(false);

        GameStateStack.Push(GameState.OverWorld);
        BeastBaseDB.Init();
        MoveDB.Init();
        Debug.Log("Howdy");


        Beast playerBeast = Area.getBeastPerRoute(AreaID.Route101);
        //playerBeast.CheckAllStats();
        playerBeast.IsPlayerUnit = true;
        Player.Party.Add(playerBeast);
        Debug.Log("Party Name " + Player.Party[0].Name);


    }

    // Update is called once per frame
    void Update()
    {
        if(GameStateStack.Peek() == GameState.OverWorld)
        {
            BattleSystemMB.BattleDialogBoxMB.DisplayBattleDialogTextNoAnimation("Press V To Start New Battle");
            Debug.Log("OverWorld");
            if (Input.GetKeyDown(KeyCode.V))
            {
                BattleSystemMB.WildBeast = Area.getBeastPerRoute(AreaID.Route101);
                BattleSystemMB.BattleStateStack.Push(BattleState.StartBattle);
                GameStateStack.Push(GameState.Battle);
                BattleSystemMB.isWildBattle = true;
            }
        }
        else if (GameStateStack.Peek() == GameState.Battle)
        {
            
            BattleSystemMB.HandleGameStateBattle();
            Debug.Log("Battle");
            
            //Debug.Log("GS: " + GameStateStack.Peek());
        }
    }
}
