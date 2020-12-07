using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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

    // Reference to character's text name on the UI
    public Text nameText;

    // Reference to characters nametag above the character
    public GameObject nameTag;

    // Reference to character's health and stress bars
    public HealthBar healthBar;
    public StressBar stressBar;
    public Animator animator;


    // buffs/debuffs
    public int atkUp = 0;
    public int stressDown = 0;

    void Awake()
    {
        controller = GameObject.Find("EventController").GetComponent<EventController>();
        skillManager = GameObject.Find("EventController").GetComponent<SkillManager>();
        origin = transform.position;
        if (!isEnemy)
        {
            Quaternion rotation = new Quaternion(0, 0, 0, 0);
            nameTag = GameObject.Instantiate(Resources.Load("NameTag"), transform.position, rotation) as GameObject;
            nameTag.transform.Find("Name").GetComponent<TextMesh>().text = characterName;
        }
        animator = GetComponent<Animator>();
        animator.SetInteger("health", health);


    }

    void Start()
    {

        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(health);


        if (!isEnemy)
        {
            SetNameText();
            stressBar.SetMaxStress(maxStress);
            stressBar.SetStress(stress);
        }


    }


    void OnMouseEnter()
    {
        if (isTargetable)
        {
            transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            animator.Play("hover");
            nameText.color = Color.yellow;

            //transform.position = transform.position + new Vector3(0, 0.1f, 0);
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

    private void SetNameText()
    {
        nameText.text = characterName;
    }

    public int GetAttack()
    {
        if (atkUp > 0)
        {
            return (int)Math.Ceiling(attack * 1.5);
        }
        else
        {
            return attack;
        }
    }

    public void resetScale()
    {
        transform.localScale = new Vector3(1, 1, 1);
        transform.position = origin;
        animator.Play("empty");
        HighlightPlayerName(false);
        if (controller.playerTurn)
        {
            controller.players[controller.turnNum].HighlightPlayerName(true);
        }

    }

    public void SetStatus(string status, int duration)
    {
        switch (status)
        {
            case "atkUp":
                atkUp = duration;
                if (atkUp > 0)
                {
                    nameTag.transform.Find("AtkUp").GetComponent<TextMesh>().text = "Atk Up (" + atkUp + ")";
                    nameTag.transform.Find("AtkUp").gameObject.SetActive(true);
                }
                else
                {
                    nameTag.transform.Find("AtkUp").gameObject.SetActive(false);
                }
                break;
            case "stressDown":
                stressDown = duration;
                if (stressDown > 0)
                {
                    nameTag.transform.Find("StressDown").gameObject.SetActive(true);
                }
                else
                {
                    nameTag.transform.Find("StressDown").gameObject.SetActive(false);
                }
                break;
        }
    }


    // Set this character's current health
    public void SetCharacterHealth(int health)
    {

        this.health = (health > 0) ? health : 0;
        healthBar.SetHealth(this.health);
        if (!isEnemy)
        {
            animator.SetInteger("health", this.health);
        }

    }

    public void takeDamage(int damage)
    {
        int d = damage;
        if (stress >= 70)
        {
            d = (int)(d * 1.3);
        }
        SetCharacterHealth(health - d);

        //controller.DisplayDamage(this, d);
        controller.DisplayHealthChange(this, d, Color.red);
    }

    public void healHealth(double amount)
    {
        int heal = (amount + health <= maxHealth) ? (int) amount : maxHealth - health;
        SetCharacterHealth(heal + health);
        if (!isEnemy)
        {
            healthBar.SetHealth(this.health);
        }
        //controller.DisplayHeal(this, heal);
        controller.DisplayHealthChange(this, heal, Color.green);
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

        if (this.stress >= 70)
        {
            nameTag.transform.Find("Stressed").gameObject.SetActive(true);
            animator.Play("stressed");
        }
        else
        {
            nameTag.transform.Find("Stressed").gameObject.SetActive(false);
            animator.Play("notstressed");
        }
    }

    public void AddStress(int stress)
    {
        if (stressDown > 0)
        {
            SetCharacterStress(((stress - 1) / 2) + 1 + this.stress);
        }
        else
        {
            SetCharacterStress(stress + this.stress);
        }

    }

    public void DisplayTurnMarker(bool enabled)
    {
        if (!isEnemy)
        {
            marker.SetActive(enabled);
        }
    }

    public void HighlightPlayerName(bool isTurn)
    {
        if (isTurn)
        {
            nameText.color = Color.red;
        }
        else
        {
            nameText.color = Color.white;
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
        if (skip != -1)
        {
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
                        stats.attack += 3;
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
                        stats.defense += 2;
                    }
                    else if (i % 2 == 0)
                    {
                        stats.health += 3;
                        stats.attack += 2;
                        stats.defense += 2;
                    }
                    else
                    {
                        stats.health += 3;
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
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(health);
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