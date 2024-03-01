using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnitUI
{
    //GameObject EnemyHPObject;
    //public Text EnemyHPText;
    public int Hi = 20;
    private void Awake()
    {
        //EnemyHPObject = GameObject.Find("Enemy HP");
        //Debug.Log("EnemyHPText " + EnemyHPText);
        //EnemyHPText = EnemyHPObject.GetComponent<Text>();
        //Debug.Log("EnemyHPText " + EnemyHPObject);
    }


    //public Text EnemyHPText;


    public void SetupEnemy(Beast wildBeast)
    {
        //EnemyHPObject = GameObject.Find("Enemy HP");
        GameObject.Find("Enemy HP").GetComponent<TMPro.TextMeshProUGUI>().text = wildBeast.CurrentHP.ToString();
        //EnemyHPText.text = "Howdy";//wildBeast.CurrentHP.ToString();
    }

    public void SetupPlayer(Beast wildBeast)
    {
        
        GameObject.Find("Player HP").GetComponent<TMPro.TextMeshProUGUI>().text = wildBeast.CurrentHP.ToString();
        
    }


    //public Image EnemyHP = GameObject.Find("Enemy HP").GetComponent<Image>();


}
