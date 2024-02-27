using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move 
{
    public int power { get; set; }
    public int accuracy { get; set; }
    public string typing { get; set; }

    public Move()
    {

    }

}

//Instantiate all moves at beginning of game and put them in a DataBase
//put them in a DataBase, and then instantiate the DataBase