using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct SkillDatabase
{
    public List<SkillDetail> skills;
}


// class for each skill
public class Skill : Action
{
    public List<SkillConditions> conditions;

    public List<SkillConditions> Conditions { get => conditions; }

    public Skill(string name, string description,
        string targetType, string alias, List<SkillConditions> conditions)
        : base(name, description, targetType, alias)
    {
        this.conditions = conditions;
    }

    // perform an action for the item
    // user is the person casting it
    // targets are the targets of the skill
    override public IEnumerator performAction(Character user, Character[] targets)
    {
        Character target = targets[0];

        EventController controller = GameObject.Find("EventController").GetComponent<EventController>();

        switch (name)
        {
            case "nothing":
                yield return new WaitForSeconds(.5f);
                break;

            case "basicAttack":
                target.takeDamage(calcDamage(user.attack, target.defense));
                user.GetComponent<Animator>().Play("attack", 0, 0);
                target.GetComponent<Animator>().Play("hurt", 0, 0);
                yield return new WaitForSeconds(.8f);
                break;

            case "slimeAttack":
                target.takeDamage(calcDamage(user.attack * 1.5, target.defense));
                user.GetComponent<Animator>().Play("attack", 0, 0);
                target.GetComponent<Animator>().Play("hurt", 0, 0);
                yield return new WaitForSeconds(.8f);
                break;

            case "strongAttack":
                target.takeDamage(calcDamage(user.attack * 2, target.defense));
                user.GetComponent<Animator>().Play("attack", 0, 0);
                target.GetComponent<Animator>().Play("hurt", 0, 0);
                user.SetCharacterStress(user.stress + 30);
                yield return new WaitForSeconds(.8f);
                break;

            case "medAttack":
                target.takeDamage(calcDamage(user.attack * 1.5, target.defense));
                user.GetComponent<Animator>().Play("attack", 0, 0);
                target.GetComponent<Animator>().Play("hurt", 0, 0);
                user.SetCharacterStress(user.stress + 10);
                yield return new WaitForSeconds(.8f);
                break;

            case "healTarget":
                target.healHealth(20 + user.attack);
                user.GetComponent<Animator>().Play("attack", 0, 0);
                user.SetCharacterStress(user.stress + 30);
                yield return new WaitForSeconds(.8f);
                break;

            case "bookSlam":
                user.GetComponent<Animator>().Play("attack", 0, 0);
                foreach (Character t in ((user.isEnemy) ? controller.GetAlivePlayers() : controller.getEnemies()))
                {
                    t.takeDamage(calcDamage(user.attack * 0.6, t.defense));
                    t.GetComponent<Animator>().Play("hurt", 0, 0);
                }
                yield return new WaitForSeconds(.8f);
                break;

            case "wideAttack":
                user.GetComponent<Animator>().Play("attack", 0, 0);
                user.SetCharacterStress(user.stress + 40);
                foreach (Character t in ((user.isEnemy) ? controller.GetAlivePlayers() : controller.getEnemies()))
                {
                    t.takeDamage(calcDamage(user.attack, t.defense));
                    t.GetComponent<Animator>().Play("hurt", 0, 0);
                }
                yield return new WaitForSeconds(.8f);
                break;

        }
    }
}

[Serializable]
public struct SkillDetail
{
    public List<SkillConditions> conditions;
    public string name; // the name that we use within the coding program
    public string alias; // the name that the user sees in the UI
    public string description;
    public string targetType;
}

// class for conditions before choosing a target, such as checking mana/stress/hp costs 
[Serializable]
public struct SkillConditions
{
    public string name; // eg. minMana, minHealth, maxStress
    public double value; // Numerical value
}