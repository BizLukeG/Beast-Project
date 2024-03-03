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
    //public BattleSystem BattleSystem;

    
    void Awake()
    {
        //BattleSystem = new BattleSystem();
        //BattleSystemGO = GameObject.Find("BattleSystem");
        //BattleSystem = BattleSystemGO.GetComponent<BattleSystem>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        GameStateStack.Push(GameState.OverWorld);
        BeastBaseDB.Init();
        MoveDB.Init();
        Debug.Log("Howdy");


        Beast playerBeast = Area.getBeastPerRoute(AreaID.Route101);
        playerBeast.CheckAllStats();
        Player.Party.Add(playerBeast);
        Debug.Log("Party Name " + Player.Party[0].Name);


    }

    // Update is called once per frame
    void Update()
    {
        if(GameStateStack.Peek() == GameState.OverWorld)
        {
            Debug.Log("OverWorld");
            if (Input.GetKeyDown(KeyCode.K))
            {
                BattleSystem.WildBeast = Area.getBeastPerRoute(AreaID.Route101);
                BattleSystem.BattleStateStack.Push(BattleState.StartBattle);
                GameStateStack.Push(GameState.Battle);
                BattleSystem.isWildBattle = true;
            }
        }
        else if (GameStateStack.Peek() == GameState.Battle)
        {
            
            BattleSystem.HandleGameStateBattle();
            Debug.Log("Battle");
            if (Input.GetKeyDown(KeyCode.J))
            {
                GameStateStack.Pop();
            }
            //Debug.Log("GS: " + GameStateStack.Peek());
        }
    }
}
