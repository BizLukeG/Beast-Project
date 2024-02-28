using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move 
{
    public int Power { get; set; }
    public int Accuracy { get; set; }
    public Typing Typing { get; set; }
    public MoveID Name { get; set; }
    

    public Move()
    {

    }

}

//Instantiate all moves at beginning of game and put them in a DataBase
//put them in a DataBase, and then instantiate the DataBase