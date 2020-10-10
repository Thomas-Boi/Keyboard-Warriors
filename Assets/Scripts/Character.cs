using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Character's stats
    public string characterName;
    public float level;
    public float maxHealth;
    public float health;
    public float maxStress;
    public float stress;
    public float attack;
    public float defense;
    public string skills; // see useSkill method

    public bool isTargetable = false;
    public bool isEnemy = true;

    private EventController controller;
    public Spawner parent;

    // Reference to character's health and stress bars
    public HealthBar healthBar;
    public StressBar stressBar;

    void Awake()
    {
        controller = GameObject.Find("EventController").GetComponent<EventController>();

        
    }

    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(health);

        if (!isEnemy)
        {
            stressBar.SetMaxStress(maxStress);
            stressBar.SetStress(stress);
        }
    }

    void OnMouseEnter()
    {

        if (isTargetable)
        {
            transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            transform.position = transform.position + new Vector3(0, 0.3f, 0);
        }
        

    }


    // ...and the mesh finally turns white when the mouse moves away.
    void OnMouseExit()
    {
        if (isTargetable)
        {
            resetScale();
        }
        
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && isTargetable)
        {

            resetScale();
            StartCoroutine(useSkill(controller.players[controller.turnNum], this, controller.selectedSkill));
            isTargetable = false;
        }
    }


    public void resetScale()
    {
        transform.localScale = new Vector3(1, 1, 1);
        transform.position = parent.transform.position;
    }

    public Vector3 getCharacterPosition()
    {
        return transform.position;
    }

    public static T chooseRandom<T>(Dictionary<T, int> dict)
    {
        System.Random rand = new System.Random();
        int value = rand.Next(dict.Values.Sum());
        int currentVal = 0;
        foreach(KeyValuePair<T, int> entry in dict)
        {
            currentVal += entry.Value;
            if (currentVal > value)
            {
                return entry.Key;
            }

        }
        return default(T);
    }

    // AI skill selection
    public void selectSkill()
    {

        //todo: make this read from a file/database instead of a switch/case statement
        Dictionary<string, int> choices = new Dictionary<string, int>();
        switch (skills)
        {
            case "slime":
                choices.Add("basicAttack", 2);
                choices.Add("slimeyAttack", 1);
                break;

        }
        selectTarget(chooseRandom(choices));
    }

    //AI target selection
    public void selectTarget(string skill)
    {
        Dictionary<Character, int> choices = new Dictionary<Character, int>();

        //targeting types
        string[] enemies = { "basicAttack", "slimeyAttack" };
        string[] allies = { };



        if (enemies.Contains(skill))
        {
            
            foreach (Character player in controller.players)
            {
                if (player.health > 0) // check if player is alive
                {
                    choices.Add(player, 1);
                    
                }
                
            }
        } else if (allies.Contains(skill))
        {
            foreach (Spawner enemySpawner in controller.spawners)
            {
                if (enemySpawner.isOccupied) // check if enemy exists/is alive
                {
                    choices.Add(enemySpawner.enemy, 1);
                }
                
            }
        }

        StartCoroutine(useSkill(this, chooseRandom(choices), skill));
    }

    // Set this character's current health
    public void SetCharacterHealth(float health)
    {
        healthBar.SetHealth(health);
        this.health = health;
    }

    public void SetCharacterStress(float stress)
    {
        stressBar.SetStress(stress);
        this.stress = stress;
    }

    public IEnumerator waitForAnim(string animation)
    {
        GetComponent<Animator>().Play(animation, 0, 0);
        yield return new WaitForSeconds(1);
    }

    public IEnumerator useSkill(Character user, Character target, string skill)
    {
        float damage = 0;
        controller.hideSkills();
        //todo: Make animation stuff a function
        switch (skill)
        {
            case "basicAttack":
                damage = user.attack;

                target.health -= damage;
                target.SetCharacterHealth(target.health);
                user.GetComponent<Animator>().Play("Base Layer.attack", 0, 0);
                yield return new WaitForSeconds(.5f);
                break;
            case "slimeyAttack":
                damage = (int)Math.Floor(user.attack * 1.2);

                target.health -= damage;
                target.SetCharacterHealth(target.health);
                user.GetComponent<Animator>().Play("Base Layer.attack", 0, 0);
                yield return new WaitForSeconds(.5f);
                break;
                
        }

        StartCoroutine(controller.DisplayDamage(target, damage));


        if (!isEnemy)
        {
            UnityEngine.Debug.Log("Player Stress: " + stress);
        }

        UnityEngine.Debug.Log(skill);
        UnityEngine.Debug.Log(target.characterName);
        UnityEngine.Debug.Log(target.health);

        if (controller.checkLife())
        {
            controller.nextTurn();
        }
    }

}
