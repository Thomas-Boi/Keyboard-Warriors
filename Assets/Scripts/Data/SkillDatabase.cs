using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct SkillDatabase
{
    public List<Skill> skills;
}


// class for each skill
[Serializable]
public struct Skill
{
    public string name;
    public string alias;
    public string description;
    public string targeting; // eg. "enemySingle, enemyAll, allySingle, lowestHealth"
    public List<SkillConditions> conditions;
}



// class for conditions before choosing a target, such as checking mana/stress/hp costs 
[Serializable]
public struct SkillConditions
{
    public string name; // eg. minMana, minHealth, maxStress
    public double value; // Numerical value
}