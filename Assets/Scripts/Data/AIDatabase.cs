using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct AIDatabase
{
    public List<AI> AIs;
}


// class for each skill
[Serializable]
public struct AI
{
    public string name;
    public List<SkillWeight> skills; // skills available to monster

}

[Serializable]
public struct SkillWeight
{
    public string name;
    public int weight; // chance of skill being chosen.
}