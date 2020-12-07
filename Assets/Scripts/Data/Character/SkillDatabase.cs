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
        bool playedMusic = false;
        switch (name)
        {
            case "nothing":
                user.GetComponent<Animator>().Play("stressed", 0, 0);
                break;

            case "basicAttack":
                target.takeDamage(calcDamage(user.GetAttack(), target.defense));
                user.GetComponent<Animator>().Play("attack", 0, 0);
                target.GetComponent<Animator>().Play("hurt", 0, 0);
                break;

            case "slimeAttack":
                target.takeDamage(calcDamage(user.GetAttack() * 1.5, target.defense));
                user.GetComponent<Animator>().Play("attack", 0, 0);
                target.GetComponent<Animator>().Play("hurt", 0, 0);
                break;

            case "strongAttack":
                target.takeDamage(calcDamage(user.GetAttack() * 1.5, target.defense));
                user.GetComponent<Animator>().Play("attack", 0, 0);
                target.GetComponent<Animator>().Play("hurt", 0, 0);
                user.AddStress(20);
                break;

            case "medAttack":
                target.takeDamage(calcDamage(user.GetAttack() * 1.5, target.defense));
                user.GetComponent<Animator>().Play("attack", 0, 0);
                target.GetComponent<Animator>().Play("hurt", 0, 0);
                user.AddStress(10);
                break;

            case "healTarget":
                target.healHealth(20 + user.GetAttack() * 0.5);
                user.GetComponent<Animator>().Play("attack", 0, 0);
                user.AddStress(30);
                controller.audioController.PlayHeal();
                playedMusic = true;
                break;

            case "healTeam":
                
                foreach (Character t in ((user.isEnemy) ? controller.getEnemies() : controller.GetAlivePlayers()))
                {
                    target.healHealth(15 + user.GetAttack() * 0.3);
                }
                user.GetComponent<Animator>().Play("attack", 0, 0);
                user.AddStress(45);
                controller.audioController.PlayHeal();
                playedMusic = true;
                break;

            case "healStress":
                user.GetComponent<Animator>().Play("attack", 0, 0);
                target.SetCharacterStress(user.stress - 40);
                controller.DisplayHealthChange(target, -40, Color.white);
                user.AddStress(25);
                controller.audioController.PlayHeal();
                playedMusic = true;
                break;

            case "weakHealStress":
                user.GetComponent<Animator>().Play("attack", 0, 0);
                target.SetCharacterStress(user.stress - 20);
                controller.DisplayHealthChange(target, -20, Color.white);
                user.AddStress(10);
                controller.audioController.PlayHeal();
                playedMusic = true;
                break;

            case "teamHealStress":
                user.GetComponent<Animator>().Play("attack", 0, 0);
                foreach (Character t in ((user.isEnemy) ? controller.getEnemies() : controller.GetAlivePlayers()))
                {
                    t.SetCharacterStress(user.stress - 20);
                    controller.DisplayHealthChange(t, -20, Color.white);
                }
                user.AddStress(45);
                controller.audioController.PlayHeal();
                playedMusic = true;
                break;

            case "buffAtk":
                target.SetStatus("atkUp", 1);
                user.GetComponent<Animator>().Play("attack", 0, 0);
                user.AddStress(30);
                controller.audioController.PlayBuff();
                playedMusic = true;
                break;
            case "selfBuffAtk":
                target.SetStatus("atkUp", 1);
                user.GetComponent<Animator>().Play("attack", 0, 0);
                user.AddStress(25);
                controller.audioController.PlayBuff();
                playedMusic = true;
                break;

            case "bookSlam":
                user.GetComponent<Animator>().Play("attack", 0, 0);
                foreach (Character t in ((user.isEnemy) ? controller.GetAlivePlayers() : controller.getEnemies()))
                {
                    t.takeDamage(calcDamage(user.GetAttack() * 0.6, t.defense));
                    t.GetComponent<Animator>().Play("hurt", 0, 0);
                }
                break;

            case "wideAttack":
                user.GetComponent<Animator>().Play("attack", 0, 0);
                foreach (Character t in ((user.isEnemy) ? controller.GetAlivePlayers() : controller.getEnemies()))
                {
                    t.takeDamage(calcDamage(user.GetAttack() * 0.9, t.defense));
                    t.GetComponent<Animator>().Play("hurt", 0, 0);
                }
                user.AddStress(45);
                break;
            case "weakWideAttack":
                user.GetComponent<Animator>().Play("attack", 0, 0);
                foreach (Character t in ((user.isEnemy) ? controller.GetAlivePlayers() : controller.getEnemies()))
                {
                    t.takeDamage(calcDamage(user.GetAttack() * 0.8, t.defense));
                    t.GetComponent<Animator>().Play("hurt", 0, 0);
                }
                user.AddStress(45);
                break;
            case "strongWideAttack":
                user.GetComponent<Animator>().Play("attack", 0, 0);
                foreach (Character t in ((user.isEnemy) ? controller.GetAlivePlayers() : controller.getEnemies()))
                {
                    t.takeDamage(calcDamage(user.GetAttack() * 1.5, t.defense));
                    t.GetComponent<Animator>().Play("hurt", 0, 0);
                }
                user.AddStress(70);
                break;
        }


        if (!playedMusic)
        {
            controller.audioController.PlayHit();
        }
        yield return new WaitForSeconds(.8f);
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