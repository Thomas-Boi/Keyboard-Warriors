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




    public void aiSelectSkill(Character user)
    {

        Dictionary<string, int> choices = new Dictionary<string, int>();

        AI ai = aiData.AIs.Find(x => x.name == user.ai);
        if (ai == default(AI))
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
                    choices.Add(target, 1);
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
        Skill skill = getSkillByName(skillName);
        controller.hideSkills();
        controller.clearDescription();

        int damage = 0;
        //todo: Make animation stuff a function
        controller.DisplaySkillDialogue(user, skill.alias, 1.0f);
        switch (skillName)
        {
            case "nothing":
                yield return new WaitForSeconds(.5f);
                break;

            case "basicAttack":
                damage = user.attack;

                target.health -= damage;
                target.SetCharacterHealth(target.health);
                user.GetComponent<Animator>().Play("attack", 0, 0);
                yield return new WaitForSeconds(.5f);
                break;

            case "slimeAttack":
                damage = (int)Math.Floor(user.attack * 1.5);

                target.health -= damage;
                target.SetCharacterHealth(target.health);
                user.GetComponent<Animator>().Play("attack", 0, 0);
                yield return new WaitForSeconds(.5f);
                break;

            case "strongAttack":
                damage = user.attack * 2;

                target.health -= damage;
                target.SetCharacterHealth(target.health);
                user.GetComponent<Animator>().Play("attack", 0, 0);
                yield return new WaitForSeconds(.5f);
                break;

        }
        UnityEngine.Debug.Log(user.name);
        
        controller.DisplayDamage(target, damage);
        controller.CheckStressChange(user);


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
