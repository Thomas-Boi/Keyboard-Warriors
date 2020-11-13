using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public struct WeekDatabase
{
    public List<Week> weeks;
}


[Serializable]
public struct Week
{
    public int weekNum;
    public List<Wave> waves;
}


[Serializable]
public struct Wave
{
    public List<string> enemies;
    public int coins;
    public int exp;
    
}