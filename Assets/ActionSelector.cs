using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionSelector : MonoBehaviour
{
    static int currentAction;
    static TMPro.TextMeshProUGUI[] actionTexts;
    static Color highlightedColor = new Color(0.3f, 0.4f, 0.6f);
    BattleSystem BattleSystemMB;

    void Awake()
    {
        actionTexts = new TMPro.TextMeshProUGUI[] { GameObject.Find("Fight").GetComponent<TMPro.TextMeshProUGUI>(),  GameObject.Find("Bag").GetComponent<TMPro.TextMeshProUGUI>(),
        GameObject.Find("Party").GetComponent<TMPro.TextMeshProUGUI>(), GameObject.Find("Run").GetComponent<TMPro.TextMeshProUGUI>()
        };
        BattleSystemMB = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();
    }


    public void HandleBattleStateActionSelection(){
        Debug.Log("HBSAS");
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
            ++currentAction;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            --currentAction;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            currentAction += 2;
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            currentAction -= 2;
        //puts a cap on current action from between 0-3
        currentAction = Mathf.Clamp(currentAction, 0, 3);
        UpdateActionSelection(currentAction);

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (currentAction == 0)
            {
                // Fight
                
                BattleSystemMB.BattleStateStack.Push(BattleState.MoveSelection);
            }
            else if (currentAction == 1)
            {
                // Bag
                //OpenBag();
                //StartCoroutine(RunTurns(BattleAction.UseItem));
            }
            else if (currentAction == 2)
            {
                // Party
                //prevState = state;
                //OpenPartyScreen();
            }
            else if (currentAction == 3)
            {
                // Run
                //StartCoroutine(RunTurns(BattleAction.Run));
            }

        }

    }

    static public void UpdateActionSelection(int selectedAction)
    {
        for (int i = 0; i < actionTexts.Length; ++i)
        {
            if (i == selectedAction)
                actionTexts[i].color = highlightedColor;
            else
                actionTexts[i].color = Color.black;
        }
    }
}
