using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SkillDatabase
{
    public List<Skill> skills = new List<Skill>();
}


// class for each skill
[Serializable]
public class Skill
{
    public string name;
    public string alias;
    public string description;
    public string targeting; // eg. "enemySingle, enemyAll, allySingle, lowestHealth"
    public List<SkillConditions> conditions = new List<SkillConditions>();
}



// class for conditions before choosing a target, such as checking mana/stress/hp costs 
[Serializable]
public class SkillConditions
{
    public string name; // eg. minMana, minHealth, maxStress
    public double value; // Numerical value
}