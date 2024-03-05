using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDialogBox : MonoBehaviour
{
    TMPro.TextMeshProUGUI battleDialogText;
    public bool IsTyping { get; set; } = false;

    void Awake()
    {
        battleDialogText = GameObject.Find("Battle Dialog Text").GetComponent<TMPro.TextMeshProUGUI>();
    }

    public IEnumerator DisplayBattleDialogText(string dialog)
    {
        IsTyping = true;
        battleDialogText.text = "";
        //var timer = new System.Windows.Threading.DispatcherTimer();
        //int x = 0;
        foreach (var letter in dialog.ToCharArray())
        {

            battleDialogText.text += letter;
            
            yield return new WaitForSeconds(1f / 30);
        }
        //yield return new WaitForSeconds(1f);
        IsTyping = false;
    }

    public void DisplayBattleDialogTextNoAnimation(string dialog)
    {
        
        battleDialogText.text = dialog;
        
    }
}
