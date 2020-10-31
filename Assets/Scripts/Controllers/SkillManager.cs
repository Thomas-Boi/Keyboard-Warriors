using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SkillManager : MonoBehaviour
{
    public TextAsset skillJson;
    public TextAsset aiJson;
    public SkillDatabase skillData;
    private AIDatabase aiData;
    private EventController controller;



    void Start()
    {
        skillData = JsonUtility.FromJson<SkillDatabase>(skillJson.text);
        aiData = JsonUtility.FromJson<AIDatabase>(aiJson.text);
        controller = GetComponent<EventController>();
    }


    public static T chooseRandom<T>(Dictionary<T, int> dict)
    {
        System.Random rand = new System.Random();
        int value = rand.Next(dict.Values.Sum());
        int currentVal = 0;
        foreach (KeyValuePair<T, int> entry in dict)
        {
            currentVal += entry.Value;
            if (currentVal > value)
            {
                return entry.Key;
            }

        }
        return default(T);
    }

    //calculates damage after defense formula
    public static int calcDamage(double damage, int defense)
    {
        return (int)(damage * (50d / (50d + defense)));
    }


    public void aiSelectSkill(Character user)
    {

        Dictionary<string, int> choices = new Dictionary<string, int>();

        AI ai = aiData.AIs.FirstOrDefault(x => x.name == user.ai);
        if (object.Equals(ai, default(AI)))
        {
            choices.Add("nothing", 1);
        }
        else
        {
            foreach (SkillWeight skill in ai.skills)
            {
                if (validConditions(user, skill.name) && validTargetExists(user, skill.name))
                {
                    choices.Add(skill.name, skill.weight);
                }

            }

        }

        if (choices.Values.Sum() == 0)
        {
            choices.Add("nothing", 1);
        }


        aiSelectTarget(user, chooseRandom(choices));
    }

    public void aiSelectTarget(Character user, string skillName)
    {
        Dictionary<Character, int> choices = new Dictionary<Character, int>();
        Skill skill = getSkillByName(skillName);

        switch (skill.targeting)
        {
            case "self":
                choices.Add(user, 1);
                break;

            case "enemySingle":
                List<Character> targets = (user.isEnemy) ? controller.players : controller.getEnemies();
                foreach (Character target in targets)
                {
                    if (target.health > 0)
                    {
                        choices.Add(target, 1);
                    }
                }
                break;

            default:
                choices.Add(user, 1);
                break;
        }


        StartCoroutine(useSkill(user, chooseRandom(choices), skillName));
    }

    public IEnumerator useSkill(Character user, Character target, string skillName)
    {
        controller.resetTargetting();
        Skill skill = getSkillByName(skillName);
        controller.HideButtons();
        controller.clearDescription();

        //todo: Make animation stuff a function
        controller.DisplaySkillDialogue(user, skill.alias, 1.0f);
        switch (skillName)
        {
            case "nothing":
                yield return new WaitForSeconds(.5f);
                break;

            case "basicAttack":
                target.takeDamage(calcDamage(user.attack, target.defense));
                user.GetComponent<Animator>().Play("attack", 0, 0);
                yield return new WaitForSeconds(.5f);
                break;

            case "slimeAttack":
                target.takeDamage(calcDamage(user.attack * 1.5, target.defense));
                user.GetComponent<Animator>().Play("attack", 0, 0);
                yield return new WaitForSeconds(.5f);
                break;

            case "strongAttack":
                target.takeDamage(calcDamage(user.attack * 2, target.defense));
                user.GetComponent<Animator>().Play("attack", 0, 0);
                user.SetCharacterStress(user.stress + 30);
                yield return new WaitForSeconds(.5f);
                break;
            case "healTarget":
                target.healHealth(20 + user.attack);
                user.GetComponent<Animator>().Play("attack", 0, 0);
                user.SetCharacterStress(user.stress + 20);
                yield return new WaitForSeconds(.5f);
                break;

        }
        UnityEngine.Debug.Log(user.name);



        UnityEngine.Debug.Log(skill);
        UnityEngine.Debug.Log(target.characterName);
        UnityEngine.Debug.Log(target.health);

        if (controller.checkLife())
        {
            controller.nextTurn();

        }
    }


    // For ai to check if targets exist
    private bool validTargetExists(Character user, string skillName)
    {
        UnityEngine.Debug.Log(skillName);
        Skill skill = getSkillByName(skillName);
        UnityEngine.Debug.Log(skill.targeting);
        switch (skill.targeting)
        {
            case "self":
                return true;

            case "enemySingle":
                return true;

            default:
                return false; // invalid target
        }


    }

    //check if conditions are valid
    private bool validConditions(Character user, string skillName)
    {
        return true;
    }



    public Skill getSkillByName(string name)
    {
        return skillData.skills.Find(x => x.name == name);
    }



}
