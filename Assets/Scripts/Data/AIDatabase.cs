using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AIDatabase
{
    public List<AI> AIs = new List<AI>();
}


// class for each skill
[Serializable]
public class AI
{
    public string name;
    public List<SkillWeight> skills; // skills available to monster

}

[Serializable]
public class SkillWeight
{
    public string name;
    public int weight; // chance of skill being chosen.
}