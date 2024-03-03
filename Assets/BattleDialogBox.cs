using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDialogBox
{
    static TMPro.TextMeshProUGUI battleDialogText = GameObject.Find("Battle Dialog Text").GetComponent<TMPro.TextMeshProUGUI>();

    public static void DisplayBattleDialogText(string dialog)
    {
        battleDialogText.text = "";
        //var timer = new System.Windows.Threading.DispatcherTimer();
        int x = 0;
        foreach (var letter in dialog.ToCharArray())
        {

            battleDialogText.text += letter;
            while(x < 5000000){
                x++;
            }
            //yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        
    }
}
