using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SkillManager : ActionManager
{
    public TextAsset skillJson;
    public TextAsset aiJson;
    public SkillDatabase skillData;
    private AIDatabase aiData;

    void Start()
    {
        skillData = JsonUtility.FromJson<SkillDatabase>(skillJson.text);
        aiData = JsonUtility.FromJson<AIDatabase>(aiJson.text);
        initController();
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


    public void aiSelectSkill(Character user)
    {

        Dictionary<string, int> choices = new Dictionary<string, int>();

        AI ai = aiData.AIs.FirstOrDefault(x => x.name == user.id);
        if (object.Equals(ai, default(AI)))
        {
            choices.Add("nothing", 1);
        }
        else
        {
            foreach (SkillWeight skillWeight in ai.skills)
            {
                Skill skill = getSkillByName(skillWeight.name);
                if (validConditions(user, skill) && validTargetExists(user, skill))
                {
                    choices.Add(skillWeight.name, skillWeight.weight);
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
        

        switch (skill.TargetType)
        {
            case TargetType.SELF:
                choices.Add(user, 1);
                break;

            case TargetType.ENEMY_SINGLE:
                List<Character> targets = (user.isEnemy) ? controller.players : controller.getEnemies();
                foreach (Character target in targets)
                {
                    if (target.health > 0)
                    {
                        choices.Add(target, 1);
                    }
                }
                break;

            case TargetType.ENEMY_ALL:
                choices.Add(user, 1);
                break;

            default:
                choices.Add(user, 1);
                break;
        }


        StartCoroutine(useAction(user, chooseRandom(choices), skill));
    }


    // For ai to check if targets exist
    private bool validTargetExists(Character user, Skill skill)
    {
        switch (skill.TargetType)
        {
            case TargetType.SELF:
                return true;

            case TargetType.ENEMY_SINGLE:
                return true;

            case TargetType.ENEMY_ALL:
                return true;

            default:
                return false; // invalid target
        }


    }

    //check if conditions are valid
    private bool validConditions(Character user, Skill skill)
    {
        return true;
    }



    public Skill getSkillByName(string name)
    {
        SkillDetail details = skillData.skills.Find(x => x.name == name);

        return new Skill(details.name, details.description,
            details.targetType, details.alias, details.conditions);
    }



}
