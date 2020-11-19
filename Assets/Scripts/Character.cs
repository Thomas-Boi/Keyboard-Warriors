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
    public int level;
    public int exp = 0;
    public int maxHealth;
    public int health;
    public int maxStress;
    public int stress;
    public int attack;
    public int defense;
    public string id;

    public bool isTargetable = false;
    public bool isEnemy = true;

    public Vector3 origin;

    private SkillManager skillManager;
    private EventController controller;
    public GameObject marker;
    public Spawner parent;

    // Reference to character's health and stress bars
    public HealthBar healthBar;
    public StressBar stressBar;

    void Awake()
    {
        controller = GameObject.Find("EventController").GetComponent<EventController>();
        skillManager = GameObject.Find("EventController").GetComponent<SkillManager>();
        origin = transform.position;
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
            transform.position = transform.position + new Vector3(0, 0.1f, 0);
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
            controller.actionMenu.DeselectAllButtons();
            foreach (Spawner spawn in controller.spawners)
            {
                if (spawn.isOccupied)
                {
                    spawn.enemy.isTargetable = false;
                }
            }
            resetScale();
            StartCoroutine(skillManager.useAction(controller.players[controller.turnNum], this, controller.selectedAction));

        }
    }


    public void resetScale()
    {
        transform.localScale = new Vector3(1, 1, 1);
        transform.position = origin;
    }

    // Set this character's current health
    public void SetCharacterHealth(int health)
    {

        this.health = (health > 0) ? health : 0;
        healthBar.SetHealth(this.health);
    }

    public void takeDamage(int damage)
    {
        int d = damage;
        if (stress >= 70) {
            d = (int) (d * 1.3);
        }
        SetCharacterHealth(health - d);
        controller.DisplayDamage(this, d);
    }

    public void healHealth(int amount)
    {
        int heal = (amount + health <= maxHealth) ? amount : maxHealth - health;
        SetCharacterHealth(heal + health);
        controller.DisplayHeal(this, heal);
    }

    // Set this character's current stress
    public void SetCharacterStress(int stress)
    {
        if (stress > maxStress)
        {
            this.stress = maxStress;
        }
        else if (stress < 0)
        {
            this.stress = 0;
        }
        else
        {
            this.stress = stress;
        }
        stressBar.SetStress(this.stress);
    }

    public void DisplayTurnMarker(bool enabled)
    {
        if (!isEnemy)
        {
            marker.SetActive(enabled);
        }
    }

    public static int CalcExpToLevel(int currentLevel)
    {
        return (int)((100 + (currentLevel - 1) * 10) * Math.Pow(1.15, currentLevel - 1));
    }


    public static List<int> CalcNextLevel(int currentLevel, int currentExp)
    {
        // Exp formula
        int expToLevel = CalcExpToLevel(currentLevel);

        if (currentExp >= expToLevel)
        {
            return CalcNextLevel(currentLevel + 1, currentExp - expToLevel);
        }
        else
        {
            return new List<int> { currentLevel, currentExp };
        }

    }


    public Stats CalcStats(int addExp, int skip = -1)
    {
        Stats stats = new Stats();
        stats.health = 0;
        stats.attack = 0;
        stats.defense = 0;

        List<int> newLevel = CalcNextLevel(level, exp + addExp);
        if (skip != -1) {
            newLevel[0] = skip;
        }
        for (int i = level; i < newLevel[0]; i++)
        {
            switch (id)
            {
                case "dps":
                    if (i % 10 == 0)
                    {
                        stats.health += 5;
                        stats.attack += 5;
                        stats.defense += 3;
                    }
                    else if (i % 5 == 0)
                    {
                        stats.health += 4;
                        stats.attack += 4;
                        stats.defense += 1;
                    }
                    else if (i % 2 == 0)
                    {
                        stats.health += 3;
                        stats.attack += 2;
                        stats.defense += 1;
                    }
                    else
                    {
                        stats.health += 2;
                        stats.attack += 3;
                        stats.defense += 2;
                    }
                    break;

                case "tank":
                    if (i % 10 == 0)
                    {
                        stats.health += 6;
                        stats.attack += 3;
                        stats.defense += 4;
                    }
                    else
                    {
                        stats.health += 5;
                        stats.attack += 2;
                        stats.defense += 3;
                    }
                    break;

                case "support":
                    if (i % 10 == 0)
                    {
                        stats.health += 5;
                        stats.attack += 3;
                        stats.defense += 3;
                    }
                    else if (i % 2 == 0)
                    {
                        stats.health += 4;
                        stats.attack += 3;
                        stats.defense += 2;
                    }
                    else
                    {
                        stats.health += 4;
                        stats.attack += 3;
                        stats.defense += 2;
                    }
                    break;

                default:
                    UnityEngine.Debug.Log(id + " Not a valid id for LevelUp()");
                    break;

            }
        }
        stats.level = newLevel[0];
        stats.exp = newLevel[1];
        return stats;
    }




    public void LevelUp(int skip = -1)
    {
        Stats stats = CalcStats(0, skip);
        level = stats.level;
        exp = stats.exp;
        health += stats.health;
        maxHealth += stats.health;
        attack += stats.attack;
        defense += stats.defense;
    }

}

[Serializable]
public struct Stats
{
    public int level;
    public int exp;
    public int health;
    public int attack;
    public int defense;
}