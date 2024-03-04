using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDialogBox : MonoBehaviour
{
    TMPro.TextMeshProUGUI battleDialogText;

    void Awake()
    {
        battleDialogText = GameObject.Find("Battle Dialog Text").GetComponent<TMPro.TextMeshProUGUI>();
    }

    public IEnumerator DisplayBattleDialogText(string dialog)
    {
        battleDialogText.text = "";
        //var timer = new System.Windows.Threading.DispatcherTimer();
        //int x = 0;
        foreach (var letter in dialog.ToCharArray())
        {

            battleDialogText.text += letter;
            
            yield return new WaitForSeconds(1f / 4);
        }
        
    }
}
