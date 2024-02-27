using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Area
{
    public int[] LevelRange { get; set; }
    public Func<int, Beast>[] AvailableBeasts { get; set; }
    //string areaName;
    bool isInArea;
    public AreaID Id { get; set; }


}
