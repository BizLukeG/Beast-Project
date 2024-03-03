using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelector 
{
    static int currentMove;
    static TMPro.TextMeshProUGUI[] moveTexts = new TMPro.TextMeshProUGUI[] { GameObject.Find("Move 1").GetComponent<TMPro.TextMeshProUGUI>(),  GameObject.Find("Move 2").GetComponent<TMPro.TextMeshProUGUI>(),
     GameObject.Find("Move 3").GetComponent<TMPro.TextMeshProUGUI>(), GameObject.Find("Move 4").GetComponent<TMPro.TextMeshProUGUI>()
    };
    static Color highlightedColor = new Color(0.3f, 0.4f, 0.6f);
    static public MoveID SelectedMove { get; set; }

    static public void HandleBattleStateMoveSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //Count gives back the total length of the list
            if (currentMove < BattleSystem.PlayerActiveBeast.MoveSet.Count - 1)
                ++currentMove;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentMove > 0)
                --currentMove;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentMove < BattleSystem.PlayerActiveBeast.MoveSet.Count - 2)
                currentMove += 2;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentMove > 1)
                currentMove -= 2;
        }
        UpdateMoveSelection(currentMove, BattleSystem.PlayerActiveBeast.MoveSet[currentMove]);

        if (Input.GetKeyDown(KeyCode.X))
        {
            
            BattleSystem.BattleStateStack.Push(BattleState.ExecuteMoves);
            //if (move.BP == 0) return;
            //dialogBox.EnableMoveSelector(false);
            //dialogBox.EnableDialogText(true);
            //StartCoroutine(RunTurns(BattleAction.Move));
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            //dialogBox.EnableMoveSelector(false);
            //dialogBox.EnableDialogText(true);
            //ActionSelection();
        }
    }

    static public void UpdateMoveSelection(int selectedMove, MoveID moveID)
    {
        for (int i = 0; i < moveTexts.Length; ++i)
        {
            if (i == selectedMove)
                moveTexts[i].color = highlightedColor;
            else
                moveTexts[i].color = Color.black;
        }
        //bpText.text = $"BP {move.BP}/{move.Base.BP}";
        //typeText.text = move.Base.Type.ToString();

        //if (move.Base.Type.ToString() == "Sacred")
        //{
        //    backgroundMoveType.color = sacredColor;
        //}
        //else if (move.Base.Type.ToString() == "Native")
        //{
        //    backgroundMoveType.color = nativeColor;
        //}
        //else if (move.Base.Type.ToString() == "Aquatic")
        //{
        //    backgroundMoveType.color = aquaticColor;
        //}
        //else if (move.Base.Type.ToString() == "Toxic")
        //{
        //    backgroundMoveType.color = toxicColor;
        //}
        //else if (move.Base.Type.ToString() == "Glacial")
        //{
        //    backgroundMoveType.color = glacialColor;
        //}
        //else if (move.Base.Type.ToString() == "Static")
        //{
        //    backgroundMoveType.color = staticColor;
        //}

    //    if (move.Base.Category.ToString() == "Physical")
    //        damageTypeText.text = "Phys.";
    //    else if (move.Base.Category.ToString() == "MetaPhysical")
    //    {
    //        damageTypeText.text = "Meta";
    //    }
    //    else if (move.Base.Category.ToString() == "Condition")
    //    {
    //        damageTypeText.text = "Cond.";
    //    }

    //    if (move.Base.HasFlag(MoveFlag.Contact))
    //    {
    //        contactTypeText.text = "Cont.";
    //    }
    //    else
    //    {
    //        contactTypeText.text = "None";
    //    }


    //    if (move.BP == 0)
    //        bpText.color = Color.red;
    //    else if (move.BP <= 10)
    //        bpText.color = Color.yellow;
    //    else
    //        bpText.color = Color.black;
    }

    static public void SetMoveNames(List<MoveID> moveIDs)
    {
        for (int i = 0; i < moveTexts.Length; ++i)
        {
            if (i < moveIDs.Count)
                moveTexts[i].text = MoveDB.Moves[moveIDs[i]].Name.ToString();
            else
                moveTexts[i].text = "-";

        }
    }
}
