using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnitUI
{
    //GameObject EnemyHPObject;
    //public Text EnemyHPText;

    public int Hi = 20;
    //private void Awake()
    //{
    //    //EnemyHPObject = GameObject.Find("Enemy HP");
    //    //Debug.Log("EnemyHPText " + EnemyHPText);
    //    //EnemyHPText = EnemyHPObject.GetComponent<Text>();
    //    //Debug.Log("EnemyHPText " + EnemyHPObject);
    //}


    //public Text EnemyHPText;


    static public void SetupEnemy(Beast beast)
    {
        //EnemyHPObject = GameObject.Find("Enemy HP");
        GameObject.Find("Enemy HP").GetComponent<TMPro.TextMeshProUGUI>().text = "Enemy Cur.HP " + beast.ModifiedStats[StatID.HP].ToString();
        GameObject.Find("Enemy Att").GetComponent<TMPro.TextMeshProUGUI>().text = "Enemy Cur.Att " + beast.ModifiedStats[StatID.Attack].ToString();
        GameObject.Find("Enemy Def").GetComponent<TMPro.TextMeshProUGUI>().text = "Enemy Cur.Def " + beast.ModifiedStats[StatID.Defense].ToString();
        GameObject.Find("Enemy Name").GetComponent<TMPro.TextMeshProUGUI>().text = "Enemy Name " + beast.Name.ToString();
        GameObject.Find("Enemy Sprite").GetComponent<Image>().sprite = beast.FrontSprite;
        //EnemyHPText.text = "Howdy";//wildBeast.CurrentHP.ToString();
    }

    static public void SetupPlayer(Beast beast)
    {
        
        GameObject.Find("Player HP").GetComponent<TMPro.TextMeshProUGUI>().text = "Player Cur.HP " + beast.ModifiedStats[StatID.HP].ToString();
        GameObject.Find("Player Att").GetComponent<TMPro.TextMeshProUGUI>().text = "Player Cur.Att " + beast.ModifiedStats[StatID.Attack].ToString();
        GameObject.Find("Player Def").GetComponent<TMPro.TextMeshProUGUI>().text = "Player Cur.Def " + beast.ModifiedStats[StatID.Defense].ToString();
        GameObject.Find("Player Name").GetComponent<TMPro.TextMeshProUGUI>().text = "Player Name " + beast.Name.ToString();
        GameObject.Find("Player Sprite").GetComponent<Image>().sprite = beast.FrontSprite;

    }


    //public Image EnemyHP = GameObject.Find("Enemy HP").GetComponent<Image>();


}
